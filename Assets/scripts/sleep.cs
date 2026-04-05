using System.Collections;
using UnityEngine;

public class sleep : MonoBehaviour
{
    disablecontrols _disablecontrols;
    private character _character;
    private daytimecontroller daytime;
    private void Awake()
    {
        _disablecontrols = GetComponent<disablecontrols>();
        _character = GetComponent<character>();
        daytime = gamemanager.instance.timecontroller;
    }

    internal void dosleep()
    {
       Debug.Log("dosleep");
       StartCoroutine(sleeproutine());
    }

    IEnumerator sleeproutine()
    {
       screentint _screentint = gamemanager.instance._screentint;
       
       _screentint.tint();
       _disablecontrols.disablecontrol();
       yield return new WaitForSeconds(2f);
       _character.fullheal();
       _character.fullrest();
       daytime.skiptomorning();
       _screentint.untint();
       yield return  new WaitForSeconds(2f);
       _disablecontrols.enablecontrol();
       
        yield return null;
    }
}
