using System;
using UnityEngine;

public class transitionarea : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.transform.CompareTag("Player"))
      {
         transform.parent.GetComponent<transition>().initiatetransition(collision.transform);
      }
   }
}
