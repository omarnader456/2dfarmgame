using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    [SerializeField] private string nameessentialscene;
    [SerializeField] private string newnamegamestartscene;
    public void exitgame()
    {
        Debug.Log("exitting  game");
        Application.Quit();
    }

    public void startnewgame()
    {
        SceneManager.LoadScene(newnamegamestartscene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameessentialscene, LoadSceneMode.Additive);
    }
}

