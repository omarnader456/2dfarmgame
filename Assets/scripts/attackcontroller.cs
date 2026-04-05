using System;
using UnityEngine;

public class attackcontroller : MonoBehaviour
{
   [SerializeField] private float offsetdistance = 3f;
   [SerializeField] private Vector2 attackareasize = new Vector2(3f,3f);

   private Rigidbody2D rgbd2d;

   private void Awake()
   {
     rgbd2d = GetComponent<Rigidbody2D>(); 
   }

   public void attack(int damage, Vector2 lastmotionvector)
   {
       Vector2 position = rgbd2d.position + lastmotionvector * offsetdistance;
       Collider2D[] targets = Physics2D.OverlapBoxAll(position, attackareasize, 0f);
       foreach (Collider2D target in targets)
       {
           damageable _damageable = target.GetComponent<damageable>();
           if (_damageable != null)
           {
               Debug.Log("attack in attackcontroller damageable is " + _damageable);
               _damageable.takedamage(damage);
           }
       }
   } 
}
