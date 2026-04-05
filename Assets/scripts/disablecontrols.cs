using System;
using UnityEngine;

public class disablecontrols : MonoBehaviour
{
  charactercontroller2d  charactercontroller;
  private toolscharactercontroller __toolscharactercontroller;
  inventorycontroller  __inventorycontroller;
  toolbarcontroller  __toolbarcontroller;
  itemcontainerinteractcontroller  __itemcontainerinteractcontroller;

  private void Awake()
  {
   charactercontroller = GetComponent<charactercontroller2d>(); 
   __toolscharactercontroller = GetComponent<toolscharactercontroller>();
   __inventorycontroller =  GetComponent<inventorycontroller>();
   __toolbarcontroller = GetComponent<toolbarcontroller>();
   __itemcontainerinteractcontroller = GetComponent<itemcontainerinteractcontroller>();
  }

  public void disablecontrol()
  {
      charactercontroller.enabled = false;
      __toolscharactercontroller.enabled = false;
      __inventorycontroller.enabled = false;
      __toolbarcontroller.enabled = false;
      __itemcontainerinteractcontroller.enabled = false;
  }

  public void enablecontrol()
  {
      charactercontroller.enabled = true;
      __toolscharactercontroller.enabled = true;
      __inventorycontroller.enabled = true;
      __toolbarcontroller.enabled = true;
      __itemcontainerinteractcontroller.enabled = true;
  }
}
