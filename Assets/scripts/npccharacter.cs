using System;
using UnityEngine;

public class npccharacter : timeagent
{
   public npcdefinition character;
   [Range(0,1f)]
   public float relationshiplevel;

   public int talkondaynumber = -1;

   public bool talktotoday;

   private void Start()
   {
      init();
      ontimetick += resettalkstate;
   }

   public void increaserelationship(float vf)
   {
      if (talktotoday == false)
      {
         relationshiplevel += vf;
         talktotoday = true;
      }
   }
   void resettalkstate(daytimecontroller _daytimecontroller)
   {
      if (_daytimecontroller.days != talkondaynumber)
      {
         talktotoday = false;
         talkondaynumber = _daytimecontroller.days;
      }
   }
}
