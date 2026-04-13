using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class npcmove : MonoBehaviour
{
    Rigidbody2D rb2d;
    public Animator animator;

    [SerializeField] private float speed = 0.8f;
    public float leasttime = 2.0f; 
    public float mosttime = 5.0f;  

    private Vector2 currentdirection;
    private Vector2 lastDirection;
    private Collider2D confinerCollider;

    public bool istalking = false;

    private enum NPCState { Idle, Walking, Talking }
    private NPCState currentstate = NPCState.Idle;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        GameObject confinerObj = GameObject.Find("cameraconfiner");
        if (confinerObj != null)
        {
            confinerCollider = confinerObj.GetComponent<Collider2D>();
        }

        lastDirection = new Vector2(0, -1); 
        StartCoroutine(brainlogic()); 
    }

    private void FixedUpdate()
    {
        if (istalking)
        {
            rb2d.linearVelocity = Vector2.zero;
            animator.SetFloat("speed", 0f);

            if (gamemanager.instance._dialoguesystem.gameObject.activeInHierarchy == false)
            {
                istalking = false;
                currentstate = NPCState.Idle;
            }
            return;
        }

        if (currentstate == NPCState.Walking && currentdirection != Vector2.zero)
        {
            rb2d.linearVelocity = currentdirection * speed;
            lastDirection = currentdirection;
        }
        else
        {
            rb2d.linearVelocity = Vector2.zero;
        }

        animator.SetFloat("horizontal", lastDirection.x);
        animator.SetFloat("vertical", lastDirection.y);
        animator.SetFloat("speed", currentstate == NPCState.Walking ? 1f : 0f);

        if (confinerCollider != null)
        {
            Vector3 clampedPos = confinerCollider.ClosestPoint(transform.position);
            if (Vector3.Distance(transform.position, clampedPos) > 0.05f)
            {
                transform.position = clampedPos;
            }
        }
    }

    private IEnumerator brainlogic()
    {
        while (true)
        {
            if (istalking)
            {
                yield return null;
                continue;
            }

            int randomaction = Random.Range(0, 2); 
            float actionduration = Random.Range(leasttime, mosttime);

            switch (randomaction)
            {
                case 0:
                    currentstate = NPCState.Idle;
                    currentdirection = Vector2.zero;
                    break;
                case 1:
                    currentstate = NPCState.Walking;
                    
                    int dir = Random.Range(0, 4);
                    if (dir == 0) currentdirection = Vector2.up;
                    else if (dir == 1) currentdirection = Vector2.down;
                    else if (dir == 2) currentdirection = Vector2.left;
                    else currentdirection = Vector2.right;
                    break;
            }

            yield return new WaitForSeconds(actionduration);
        }
    }

    public void stopfordialogue(Transform playerTransform)
    {
        istalking = true;
        currentstate = NPCState.Talking;
        rb2d.linearVelocity = Vector2.zero;

        Vector3 direction = (playerTransform.position - transform.position).normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            lastDirection = new Vector2(Mathf.Sign(direction.x), 0f);
        }
        else
        {
            lastDirection = new Vector2(0f, Mathf.Sign(direction.y));
        }

        animator.SetFloat("horizontal", lastDirection.x);
        animator.SetFloat("vertical", lastDirection.y);
        animator.SetFloat("speed", 0f);
    }
}