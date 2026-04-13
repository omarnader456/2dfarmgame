using UnityEngine;

public class enemy : MonoBehaviour, idamageable
{
    public int maxHealth = 3;
    private int currentHealth;
    
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void calculatedamage(ref int damage)
    {
    }

    public void applydamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
    }

    public void checkstate()
    {
        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        chaseenemy chaseScript = GetComponent<chaseenemy>();
        if (chaseScript != null)
        {
            chaseScript.enabled = false;
        }

        spawnedobject spawnTracker = GetComponent<spawnedobject>();
        if (spawnTracker != null)
        {
            spawnTracker.spawnedobjectdestroy();
        }

        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        Destroy(gameObject, 1f); 
    }
}