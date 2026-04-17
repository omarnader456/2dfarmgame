using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class datawrapper
{
    public List<string> objectIDs = new List<string>();
    public List<string> objectdatas = new List<string>();
}

public class dataorchestrator : MonoBehaviour
{
    public static dataorchestrator instance;
    public datawrapper currentdata = new datawrapper();
    public item[] startingitems; 
    public int[] startingitemcounts;

    private Dictionary<string, string> livedatamap = new Dictionary<string, string>();
    private bool hasinitialized = false;
    public bool isapplicationquitting { get; private set; } = false; 

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            SceneManager.sceneLoaded += onsceneloaded; 
        }
    }

    private void OnDestroy() 
    {
        if(instance == this) SceneManager.sceneLoaded -= onsceneloaded;
    }

    private void Start()
    {
        loadgamefromdisk();
        InvokeRepeating(nameof(savegame), 15f, 15f); 
    }

    public void loadgamefromdisk()
    {
        string localJson = firebasemanager.instance.readlocaldata();
        
        if (string.IsNullOrEmpty(localJson) || localJson == "{}" || !localJson.Contains("objectIDs")) {
            setupnewgame();
        } else {
            try {
                JsonUtility.FromJsonOverwrite(localJson, currentdata);
            } catch {
                setupnewgame();
                return;
            }

            livedatamap.Clear();
            for(int i = 0; i < currentdata.objectIDs.Count; i++) {
                livedatamap[currentdata.objectIDs[i]] = currentdata.objectdatas[i];
            }

            placedataintoscene(null);
        }
        hasinitialized = true;
    }

    private void onsceneloaded(Scene scene, LoadSceneMode mode)
    {
        if (hasinitialized) StartCoroutine(delayedinjection(scene));
    }
    
    private IEnumerator delayedinjection(Scene newlyloadedscene) 
    {
        yield return new WaitForEndOfFrame();
        placedataintoscene(newlyloadedscene);
    }

    public void placedataintoscene(Scene? targetscene)
    {
        var persistants = findpersistantobjects();
        foreach (var p in persistants)
        {
            MonoBehaviour mb = p as MonoBehaviour;
            if (targetscene.HasValue && mb.gameObject.scene != targetscene.Value) continue; 

            string id = getobjectID(p);
            if (livedatamap.TryGetValue(id, out string data)) {
                p.load(data);
            } else {
                p.load("{}");
            }
        }
        
        if(gamemanager.instance != null && gamemanager.instance.inventorycontainer != null) {
            gamemanager.instance.inventorycontainer.change?.Invoke();
        }
    }

    public void update_livedata(string id, string data)
    {
        livedatamap[id] = data;
    }

    public void savegame()
    {
        if (currentdata == null) currentdata = new datawrapper();

        var persistants = findpersistantobjects();
        foreach (var p in persistants) 
        {
            try 
            {
                string id = getobjectID(p);
                string newdata = p.read();

                if (!string.IsNullOrEmpty(newdata)) livedatamap[id] = newdata;
            }
            catch (Exception e)
            {
                Debug.LogError($" save error on object {p.GetType().Name}: {e.Message}");
            }
        }

        currentdata.objectIDs = livedatamap.Keys.ToList();
        currentdata.objectdatas = livedatamap.Values.ToList();

        string json = JsonUtility.ToJson(currentdata);
        firebasemanager.instance.savelocallpushfirebase(json);
    }

    private List<ipersistant> findpersistantobjects()
    {
        return FindObjectsOfType<MonoBehaviour>()
            .OfType<ipersistant>()
            .Where(obj => !((MonoBehaviour)obj).gameObject.name.Contains("(Clone)"))
            .ToList();
    }

    public string getobjectID(ipersistant obj) 
    {
        MonoBehaviour mb = obj as MonoBehaviour;
        string objectname = mb.gameObject.name;
        string typename = mb.GetType().Name;

        if (mb is inventorysaver || 
            objectname.ToLower().Contains("gamemanager") || 
            objectname.ToLower().Contains("character") || 
            mb.gameObject.scene.name == "DontDestroyOnLoad")
        {
            return objectname + "_" + typename; 
        }

        return mb.gameObject.scene.name + "_" + objectname + "_" + typename; 
    }

    private void setupnewgame()
    {
        livedatamap.Clear();
        var persistants = findpersistantobjects();
        foreach (var p in persistants) p.load("{}");

        if (gamemanager.instance != null && gamemanager.instance.inventorycontainer != null)
        {
            gamemanager.instance.inventorycontainer.init();
            if (startingitems != null)
            {
                for (int i = 0; i < startingitems.Length; i++) {
                    gamemanager.instance.inventorycontainer.add(startingitems[i], startingitemcounts[i]);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        isapplicationquitting = true;
        savegame();
    }
}