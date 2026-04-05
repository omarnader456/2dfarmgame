using System;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class npcmove : MonoBehaviour
{
   Rigidbody2D rb2d;
   public Transform moveto;
   [SerializeField] private float speed = 0.8f;
   public Animator animator;

   private void Awake()
   {
      rb2d = GetComponent<Rigidbody2D>();
      animator = GetComponentInChildren<Animator>();
   }

   private void Start()
   {
       moveto = gamemanager.instance.player.transform;
   }
   private void FixedUpdate()
   {
       if (moveto == null)
       {
           return;
       }
       if (Vector3.Distance(transform.position, moveto.position) < 0.4f)
       {
           stopmoving();
           return;
       }
     Vector3 direction = (moveto.position - transform.position).normalized;
     animator.SetFloat("horizontal", direction.x);
     animator.SetFloat("vertical", direction.y);
     direction *= speed;
     rb2d.linearVelocity = direction;
   }

   private void stopmoving()
   {
       moveto = null;
       rb2d.linearVelocity = Vector3.zero;
   }
}
