using UnityEngine;
using TMPro;

public class mainmenumanager : MonoBehaviour
{
    public GameObject loginsignuppanel;
    public GameObject mainmenupanel;
    public GameObject slotselectionpanel;
    public GameObject charactercreationpanel;
    public TMP_InputField emailinput;
    public TMP_InputField passwordinput;
    public string mainscenename = "farmscene";
    public string essentialscenename = "essential";
    public mainmenu charactercreationscript; 
    private bool isstartnewgame = false;
    private int pendingslotindex = 1; 

    private void Start()
    {
        Showpanel(loginsignuppanel);
    }

    public void loginclick()
    {
        firebasemanager.instance.signin(emailinput.text, passwordinput.text, authsuccess);
    }

    public void signupclick()
    {
        firebasemanager.instance.signup(emailinput.text, passwordinput.text, authsuccess);
    }

    private void authsuccess()
    {
        Showpanel(mainmenupanel);
    }

    public void newgameclick()
    {
        isstartnewgame = true;
        Showpanel(slotselectionpanel);
    }

    public void loadgameclick()
    {
        isstartnewgame = false;
        Showpanel(slotselectionpanel);
    }

    public void exitgameclick()
    {
        Application.Quit();
    }

    public void backtomainmenuclick()
    {
        Showpanel(mainmenupanel);
    }

    public void slotclicked(int slotindex)
    {
        pendingslotindex = slotindex;
        
        if (charactercreationscript != null)
        {
            charactercreationscript.setsavingslot(slotindex);
        }

        if (isstartnewgame)
        {
            Showpanel(charactercreationpanel);
        }
        else
        {
            firebasemanager.instance.slotselectionstartgame(slotindex, false, mainscenename, essentialscenename);
        }
    }

    public void finishcharactercreationandstart()
    {
        if (charactercreationscript != null)
        {
            charactercreationscript.updatename();
            
            playerdata finishedData = charactercreationscript.getfinishedplayerdata();
            
            firebasemanager.instance.savecharacterdatatofirebase(finishedData);
        }

        firebasemanager.instance.slotselectionstartgame(pendingslotindex, true, mainscenename, essentialscenename);
    }

    private void Showpanel(GameObject panelToShow)
    {
        loginsignuppanel.SetActive(false);
        mainmenupanel.SetActive(false);
        slotselectionpanel.SetActive(false);
        
        if (charactercreationpanel != null) 
            charactercreationpanel.SetActive(false);

        panelToShow.SetActive(true);
    }
}