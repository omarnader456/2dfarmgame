using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class charactercontroller2d : MonoBehaviour

{
    private Rigidbody2D rigidbody2d;

    [SerializeField] private float speed = 2f;
    private Vector2 motionVector;
    [SerializeField] private float runspeed = 5f;
    Animator animator;
    public Vector2 lastmotionvector;
    public bool moving;
    private bool running;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        motionVector.x = horizontal;
        motionVector.y = vertical;
        
        animator.SetFloat("horizontal",horizontal);
        animator.SetFloat("vertical", vertical);
        moving = horizontal != 0 || vertical != 0;
        animator.SetBool("moving",moving);
        if (horizontal != 0 || vertical != 0)
        {
            lastmotionvector = new Vector2(horizontal, vertical).normalized;
            animator.SetFloat("lasthorizontal",horizontal);
            animator.SetFloat("lastvertical",vertical);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.linearVelocity = motionVector * (running == true ? runspeed : speed);
    }

    private void OnDisable()
    {
       rigidbody2d.linearVelocity = Vector2.zero; 
    }
}
