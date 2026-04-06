using System.Collections;
using UnityEngine;

public class LivestockMovement : MonoBehaviour, idamageable
{
    public float normalspeed = 1.2f;
    public float damagespeed = 3.0f;
    private float currentspeed;
    public Vector2 minbounds;
    public Vector2 maxbounds;
    public float leastactiontime = 2.0f;
    public float mostactiontime = 5.0f;
    public float deadtime = 3.0f;
    public int health = 3;
    private SpriteRenderer spriterenderer;
    private Animator animator;
    private Vector2 currentdirection;
    private Animal currentstate = Animal.Idle;

    private enum Animal { Idle, Walking, Eating, Hurt, Dead }

    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentspeed = normalspeed;
        StartCoroutine(brainlogic());
    }

    void Update()
    {
        if (currentstate == Animal.Dead || currentstate == Animal.Eating) return;
        transform.Translate(currentdirection * currentspeed * Time.deltaTime);
        float clampedX = Mathf.Clamp(transform.position.x, minbounds.x, maxbounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, minbounds.y, maxbounds.y);
        transform.position = new Vector2(clampedX, clampedY);
        if (currentdirection.x > 0)
            spriterenderer.flipX = true; 
        else if (currentdirection.x < 0)
            spriterenderer.flipX = false; 
    }

    private IEnumerator brainlogic()
    {
        while (currentstate != Animal.Dead)
        {
            if (currentstate == Animal.Hurt)
            {
                yield return null;
                continue;
            }
            int randomaction = Random.Range(0, 3);
            float actionduration = Random.Range(leastactiontime, mostactiontime);

            switch (randomaction)
            {
                case 0:
                    currentstate = Animal.Idle;
                    currentdirection = Vector2.zero;
                    animator.SetBool("iswalking", false);
                    animator.SetBool("iseating", false);
                    break;

                case 1:
                    currentstate = Animal.Walking;
                    animator.SetBool("iswalking", true);
                    animator.SetBool("iseating", false);
                    
                    float directionx = Random.Range(-1, 2); 
                    float directiony = Random.Range(-1, 2);
                    currentdirection = new Vector2(directionx, directiony).normalized;
                    
                    if (currentdirection == Vector2.zero) currentdirection = Vector2.left;
                    break;

                case 2:
                    currentstate = Animal.Eating;
                    currentdirection = Vector2.zero;
                    animator.SetBool("iswalking", false);
                    animator.SetBool("iseating", true);
                    break;
            }
            yield return new WaitForSeconds(actionduration);
        }
    }


    public void calculatedamage(ref int damage)
    {
    }

    public void applydamage(int damage)
    {
        if (currentstate == Animal.Dead) return;
        health -= damage;
    }

    public void checkstate()
    {
        if (currentstate == Animal.Dead) return;
        if (health <= 0)
        {
            StartCoroutine(deathroutine());
        }
        else
        {
            StartCoroutine(damageroutine());
        }
    }


    private IEnumerator damageroutine()
    {
        currentstate = Animal.Hurt;
        animator.SetTrigger("hit"); 
        animator.SetBool("iseating", false);
        currentspeed = damagespeed;
        currentdirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        yield return new WaitForSeconds(1.5f);
        currentspeed = normalspeed;
        currentstate = Animal.Idle; 
    }

    private IEnumerator deathroutine()
    {
        currentstate = Animal.Dead;
        currentdirection = Vector2.zero; 
        animator.SetBool("iswalking", false);
        animator.SetBool("iseating", false);
        animator.SetTrigger("die"); 
        yield return new WaitForSeconds(deadtime);
        Destroy(gameObject);
    }
}