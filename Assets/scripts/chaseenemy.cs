using UnityEngine;

public class chaseenemy : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector2 attacksize = Vector2.one;
    [SerializeField] private float speed;
    [SerializeField] private int damage = 1;
    [SerializeField] private float timetoattack = 2f;
    [SerializeField] private float stopDistance = 1.2f; 

    private float attacktimer;
    private Animator animator;

    void Start()
    {
        player = gamemanager.instance.player.transform;
        attacktimer = Random.Range(0, timetoattack);
        animator = GetComponent<Animator>(); 
    }

    public void attack()
    {
        attacktimer -= Time.deltaTime;
        
        if (attacktimer > 0f)
        {
            return;
        }
        attacktimer = timetoattack;
        
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }

        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attacksize, 0f);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject == this.gameObject) continue;
            character player = targets[i].GetComponent<character>();
            damageable _damageable = targets[i].GetComponent<damageable>();
    
            if (player != null)
            {
                _damageable.takedamage(damage);
            }
        } 
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (animator != null) animator.SetBool("ismoving", true);
        }
        else
        {
            if (animator != null) animator.SetBool("ismoving", false);
            attack(); 
        }
    }
}