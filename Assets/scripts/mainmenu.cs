using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    [SerializeField] playerdata _playerdata;
    [SerializeField] Button malebutton;
    [SerializeField] Button femalebutton;
    public gender playergender;
    public TMP_InputField playername;

    public void setmalegender()
    {
        playergender = gender.Male;
        _playerdata.playergender = gender.Male; 
        malebutton.image.color = new Color32(0, 255, 0, 255);
        femalebutton.image.color = new Color32(255, 0, 0, 255);
    }

    public void setfemalegender()
    {
        playergender = gender.Female;
        _playerdata.playergender = gender.Female; 
        femalebutton.image.color = new Color32(0, 255, 0, 255);
        malebutton.image.color = new Color32(255, 0, 0, 255);
    }

    public void updatename()
    {
        _playerdata.playername = playername.text;
    }

    public void setsavingslot(int number)
    {
        _playerdata.saveslot = number;
    }

    public playerdata getfinishedplayerdata()
    {
        return _playerdata;
    }
}

