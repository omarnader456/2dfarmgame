using System;
using UnityEngine;

public class dooropenclose : MonoBehaviour
{
   [SerializeField] private GameObject opendoor;
   [SerializeField] private GameObject closedoor;

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.GetComponent<character>() != null)
      {
         _opendoor();
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.GetComponent<character>() != null)
      {
         _closedoor();
      }
   }

   private void _closedoor()
   {
      opendoor.SetActive(false);
      closedoor.SetActive(true);
   }

   private void _opendoor()
   {
      opendoor.SetActive(true);
      closedoor.SetActive(false);
   }
}
