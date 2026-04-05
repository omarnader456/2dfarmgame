using System;
using UnityEngine;

public class timeagent : MonoBehaviour
{
    public Action<daytimecontroller> ontimetick;
   private void Start()
   {
       init();
   }

   public void init()
   {
       gamemanager.instance.timecontroller.subscribe(this); 
   }
   public void Invoke(daytimecontroller _daytimecontroller)
   {
      ontimetick?.Invoke(_daytimecontroller); 
   }
   private void OnDestroy()
   {
       gamemanager.instance.timecontroller.unsubscribe(this);
   }
}
