using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class testfirebase : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencystatus = task.Result;
            if (dependencystatus == DependencyStatus.Available)
            {
                FirebaseApp firebaseapp = FirebaseApp.DefaultInstance;

                Debug.Log("Firebase is working!");
            }
            else
            {
                Debug.LogError("Firebase not working" + dependencystatus.ToString());
            }
        });
    }

   
}
