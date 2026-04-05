using System;
using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;

public class cameraconfiner : MonoBehaviour
{
   [SerializeField] private CinemachineConfiner2D confiner;

   private void Start()
   {
      updatebounds();
   }

   public void updatebounds()
   {
      GameObject obj = GameObject.Find("cameraconfiner");
      if (obj == null)
      {
         confiner.BoundingShape2D = null;
         return;
      }
      Collider2D bounds = GameObject.Find("cameraconfiner").GetComponent<Collider2D>();
      confiner.BoundingShape2D = bounds;
   }

   internal void updatebounds(Collider2D _confiner)
   {
      Debug.Log("updatebounds warp cameraconfiner is " + _confiner); 
      confiner.BoundingShape2D = _confiner;
   }

   
}
