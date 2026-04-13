using System.Collections;
using UnityEngine;

[RequireComponent(typeof(timeagent))]
[RequireComponent(typeof(Rigidbody2D))] 
public class livestockmovement : MonoBehaviour, idamageable
{
    public float normal = 1.2f;
    public float damaged = 3.0f;
    public float leasttime = 2.0f;
    public float mosttime = 5.0f;
    public int health = 30;

    private float currentspeed;
    private SpriteRenderer spriterenderer;
    private Animator animator;
    private timeagent _timeagent;
    private Rigidbody2D rb2d; 
    private Collider2D confinerCollider; 
    
    private Vector2 currentdirection;
    private Vector2 lastdirection;
    private Animal currentstate = Animal.Idle;
    private Coroutine currentflashroutine;

    private bool isnighttime = false;
    private bool iswakingupatnight = false;
    private float lastnightactionhour = 21f;

    private enum Animal { Idle, Walking, Sleeping, Hurt, Dead }

    void Start()
    {
        spriterenderer = GetComponentInChildren<SpriteRenderer>(); 
        if(spriterenderer == null) spriterenderer = GetComponent<SpriteRenderer>(); 
        
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        _timeagent = GetComponent<timeagent>();
        
        if (_timeagent != null) _timeagent.ontimetick += checktime;

        GameObject confinerObj = GameObject.Find("cameraconfiner");
        if (confinerObj != null)
        {
            confinerCollider = confinerObj.GetComponent<Collider2D>();
        }

        currentspeed = normal;
        lastdirection = new Vector2(0, -1);
        StartCoroutine(brainlogic());
    }

    void OnDestroy()
    {
        if (_timeagent != null) _timeagent.ontimetick -= checktime;
    }

    private void checktime(daytimecontroller controller)
    {
        float currenthour = controller._currenthour; 
        
        isnighttime = (currenthour >= 21f || currenthour < 5f);

        if (isnighttime)
        {
            float timediff = currenthour - lastnightactionhour;
            if (timediff < 0) timediff += 24f; 

            if (timediff >= 2f && currentstate != Animal.Hurt) 
            {
                iswakingupatnight = true;
                lastnightactionhour = currenthour;
            }
        }
        else 
        {
            lastnightactionhour = 21f; 
            iswakingupatnight = false;
        }
    }

    void FixedUpdate() 
    {
        if (currentstate == Animal.Dead)
        {
            rb2d.linearVelocity = Vector2.zero;
            return;
        }

        if (currentdirection != Vector2.zero)
        {
            rb2d.linearVelocity = currentdirection * currentspeed;
            lastdirection = currentdirection; 
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
        }

        animator.SetFloat("horizontal", lastdirection.x);
        animator.SetFloat("vertical", lastdirection.y);
        animator.SetBool("iswalking", currentstate == Animal.Walking || currentstate == Animal.Hurt);
        animator.SetBool("issleeping", currentstate == Animal.Sleeping);

        if (confinerCollider != null)
        {
            transform.position = confinerCollider.ClosestPoint(transform.position);
        }
    }

    private IEnumerator brainlogic()
    {
        while (true)
        {
            if (currentstate == Animal.Hurt || currentstate == Animal.Dead)
            {
                yield return null;
                continue;
            }

            if (isnighttime && !iswakingupatnight)
            {
                currentstate = Animal.Sleeping;
                currentdirection = Vector2.zero;
                yield return null; 
                continue;
            }

            int randomaction = Random.Range(0, isnighttime ? 2 : 3); 
            float actionduration = Random.Range(leasttime, mosttime);

            switch (randomaction)
            {
                case 0:
                    currentstate = Animal.Idle;
                    currentdirection = Vector2.zero;
                    break;
                case 1:
                    currentstate = Animal.Walking;
                    float directionx = Random.Range(-1, 2); 
                    float directiony = Random.Range(-1, 2);
                    currentdirection = new Vector2(directionx, directiony).normalized;
                    if (currentdirection == Vector2.zero) currentdirection = Vector2.left;
                    break;
                case 2:
                    currentstate = Animal.Sleeping;
                    currentdirection = Vector2.zero;
                    break;
            }
            
            yield return new WaitForSeconds(actionduration);

            if (iswakingupatnight)
            {
                iswakingupatnight = false;
            }
        }
    }

    public void calculatedamage(ref int damage)
    {
    }

    public void applydamage(int damage)
    {
        if (currentstate == Animal.Dead) return;
        
        calculatedamage(ref damage);
        health -= damage;

        if (currentstate == Animal.Sleeping)
        {
            iswakingupatnight = false;
        }

        checkstate();
    }

    public void checkstate()
    {
        if (currentstate == Animal.Dead) return;
        
        if (health <= 0)
        {
            currentstate = Animal.Dead;
            
            spawnedobject spawnTracker = GetComponent<spawnedobject>();
            if (spawnTracker != null)
            {
                spawnTracker.spawnedobjectdestroy();
            }

            Destroy(gameObject); 
        }
        else
        {
            StartCoroutine(damageroutine());
        }
    }

    private IEnumerator damageroutine()
    {
        currentstate = Animal.Hurt;
        currentspeed = damaged;
        
        currentdirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        if (currentdirection == Vector2.zero) currentdirection = Vector2.right;

        if (currentflashroutine != null) StopCoroutine(currentflashroutine);
        currentflashroutine = StartCoroutine(flashred());

        yield return new WaitForSeconds(1.5f);

        if (currentstate != Animal.Dead)
        {
            currentspeed = normal;
            currentstate = Animal.Idle; 
        }
    }

    private IEnumerator flashred()
    {
        float duration = 1.5f;
        float endTime = Time.time + duration;
        
        while (Time.time < endTime && currentstate != Animal.Dead)
        {
            if(spriterenderer != null) spriterenderer.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            if(spriterenderer != null) spriterenderer.color = Color.white;
            yield return new WaitForSeconds(0.15f);
        }
        
        if (spriterenderer != null) spriterenderer.color = Color.white;
    }
}