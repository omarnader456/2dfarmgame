using UnityEngine;

public class chaseenemy : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector2 attacksize = Vector2.one;
    [SerializeField] private float speed;

    [SerializeField] private int damage = 1;

    [SerializeField] private float timetoattack = 2f;

    private float attacktimer;
    void Start()
    {
        player = gamemanager.instance.player.transform;
        attacktimer = Random.Range(0, timetoattack);
    }

    public void attack()
    {
        attacktimer -= Time.deltaTime;
        
        if (attacktimer > 0f)
        {
            return;
        }
        attacktimer = timetoattack;
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, attacksize, 0f);
        for (int i = 0; i < targets.Length; i++)
        {
            damageable _character = targets[i].GetComponent<damageable>();
            if (_character != null)
            {
                _character.takedamage(damage);
            }
        } 
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            player.position, speed * Time.deltaTime);
       attack(); 
    }
}
