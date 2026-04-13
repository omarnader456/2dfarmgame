using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions; 
using UnityEngine.SceneManagement;

public class firebasemanager : MonoBehaviour
{
    public static firebasemanager instance;
    
    private FirebaseAuth auth;
    private FirebaseUser user;
    private DatabaseReference database;
    
    public int currentsaveslot = 1; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                database = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("firebase initialization complete.");
            }
            else
            {
                Debug.LogError("Could not resolve firebase dependencies: " + task.Result);
            }
        });
    }

    public string getlocalsavepath()
    {
        return Path.Combine(Application.persistentDataPath, $"local_save_slot_{currentsaveslot}.json");
    }

    public async void signup(string email, string password, Action success)
    {
        try {
            AuthResult result = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            user = result.User;
            success?.Invoke(); 
        } catch (Exception e) { Debug.LogError("signup failed: " + e.Message); }
    }

    public async void signin(string email, string password, Action success)
    {
        try {
            AuthResult result = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = result.User;
            success?.Invoke();
        } catch (Exception e) { Debug.LogError("login failed: " + e.Message); }
    }

    public async void slotselectionstartgame(int slotindex, bool isnewgame, string newnamegamestartscene, string nameessentialscene)
    {
        currentsaveslot = slotindex;
        string path = getlocalsavepath();

        if (isnewgame)
        {
            File.WriteAllText(path, "{}");
            if (user != null && database != null)
            {
                await database.Child("users").Child(user.UserId).Child("slot_" + currentsaveslot).SetRawJsonValueAsync("{}");
            }
        }
        else
        {
            await fetchfirebaselocalsave(path);
        }

        SceneManager.LoadScene(newnamegamestartscene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameessentialscene, LoadSceneMode.Additive);
    }

    private async Task fetchfirebaselocalsave(string path)
    {
        if (user == null || database == null) 
        {
            Debug.LogWarning("offline mode: database missing. Using local save data only.");
            if (!File.Exists(path)) File.WriteAllText(path, "{}");
            return;
        }

        try
        {
            DataSnapshot snapshot = await database.Child("users").Child(user.UserId).Child("slot_" + currentsaveslot).GetValueAsync();
            
            if (snapshot != null && snapshot.Exists)
            {
                string json = snapshot.GetRawJsonValue();
                File.WriteAllText(path, json);
            }
            else if (!File.Exists(path))
            {
                File.WriteAllText(path, "{}"); 
            }
        }
        catch(Exception e)
        {
            Debug.LogError("failed to fetch save from firebase. falling back to local.  " + e.Message);
            if (!File.Exists(path)) File.WriteAllText(path, "{}");
        }
    }

    public void savelocallpushfirebase(string jsondata)
    {
        string path = getlocalsavepath();
        try
        {
            File.WriteAllText(path, jsondata);
        }
        catch (Exception e)
        {
            Debug.LogError("failed to save locally: " + e.Message);
        }

        if (user != null && database != null)
        {
            database.Child("users").Child(user.UserId).Child("slot_" + currentsaveslot).SetRawJsonValueAsync(jsondata);
        }
    }

    public string readlocaldata()
    {
        string path = getlocalsavepath();
        if (File.Exists(path)) return File.ReadAllText(path);
        return null;
    }

    public void savecharacterdatatofirebase(playerdata data)
    {
        if (database == null) return;
        
        string characterJson = JsonUtility.ToJson(data);
        string localCharPath = Path.Combine(Application.persistentDataPath, $"local_character_slot_{currentsaveslot}.json");
        File.WriteAllText(localCharPath, characterJson);

        if (user != null)
        {
            database.Child("users").Child(user.UserId).Child("slot_" + currentsaveslot + "_character").SetRawJsonValueAsync(characterJson);
        }
    }
}