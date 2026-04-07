using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    [SerializeField] private string nameessentialscene;
    [SerializeField] private string newnamegamestartscene;
    [SerializeField] playerdata _playerdata;
    [SerializeField] Button malebutton;
    [SerializeField] Button femalebutton;
    public gender playergender;
    public TMP_InputField playername;
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

    public void setmalegender()
    {
        playergender = gender.Male;
        malebutton.image.color = new Color32(0, 255, 0, 255);
        femalebutton.image.color = new Color32(255, 0, 0, 255);
        Debug.Log(playergender);
    }

    public void setfemalegender()
    {
        playergender = gender.Female;
        femalebutton.image.color = new Color32(0, 255, 0, 255);
        malebutton.image.color = new Color32(255, 0, 0, 255);
        Debug.Log(playergender);
    }

    public void updatename()
    {
        _playerdata.playername = playername.text;
        Debug.Log(_playerdata.playername);
    }

    public void setsavingslot(int number)
    {
        _playerdata.saveslot = number;
    }
}

