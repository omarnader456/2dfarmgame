using System;
using UnityEngine;
using UnityEngine.Rendering;

public class sleeparea : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
     sleep _sleep = other.GetComponent<sleep>();
     if (_sleep != null)
     {
         _sleep.dosleep();
     }
   }
}
