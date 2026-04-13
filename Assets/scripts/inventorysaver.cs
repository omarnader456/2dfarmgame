using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class inventorysavedata
{
    public List<string> itemnames = new List<string>();
    public List<int> itemcounts = new List<int>();
}

public class inventorysaver : MonoBehaviour, ipersistant
{
    public string read()
    {
        Debug.Log("inventorysaver is attempting to read inventory");

        if (gamemanager.instance == null || gamemanager.instance.inventorycontainer == null)
        {
            Debug.LogError("gamemanager or inventorycontainer is null during a save  stopping inventory read to prevent wiping firebase data.");
            return null; 
        }

        inventorysavedata data = new inventorysavedata();
        var slots = gamemanager.instance.inventorycontainer.slots;

        if (slots != null) {
            for (int i = 0; i < slots.Count; i++) {
                if (slots[i].itm != null) {
                    data.itemnames.Add(slots[i].itm.name);
                    data.itemcounts.Add(slots[i].count);
                } else {
                    data.itemnames.Add("");
                    data.itemcounts.Add(0);
                }
            }
        }
        
        Debug.Log($"inventorysaver successfully read {slots.Count} slots.");
        return JsonUtility.ToJson(data);
    }

    public void load(string jsonstring)
    {
        if (gamemanager.instance == null || gamemanager.instance.inventorycontainer == null) return;
        
        gamemanager.instance.inventorycontainer.init();

        if (string.IsNullOrEmpty(jsonstring) || jsonstring == "{}") {
            gamemanager.instance.inventorycontainer.change?.Invoke();
            return;
        }

        inventorysavedata data = JsonUtility.FromJson<inventorysavedata>(jsonstring);
        var slots = gamemanager.instance.inventorycontainer.slots;

        if (gamemanager.instance.itemdb == null || gamemanager.instance.itemdb.items == null) return;

        for (int i = 0; i < data.itemnames.Count && i < slots.Count; i++) {
            if (!string.IsNullOrEmpty(data.itemnames[i])) {
                item foundItem = gamemanager.instance.itemdb.items.Find(x => x.name == data.itemnames[i]);
                if (foundItem != null) {
                    slots[i].set(foundItem, data.itemcounts[i]);
                } else {
                    slots[i].clear();
                }
            } else {
                slots[i].clear();
            }
        }
        
        gamemanager.instance.inventorycontainer.change?.Invoke();
        if (gameObject.activeInHierarchy) StartCoroutine(delayeduiupdate());
    }

    private IEnumerator delayeduiupdate() {
        yield return new WaitForEndOfFrame();
        if (gamemanager.instance != null && gamemanager.instance.inventorycontainer != null) {
            gamemanager.instance.inventorycontainer.change?.Invoke();
        }
    }
}