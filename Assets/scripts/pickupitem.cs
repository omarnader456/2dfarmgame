using System;
using UnityEngine;

public class pickupitem : MonoBehaviour
{
    private Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] private float pickupdistance = 1.5f;
    [SerializeField] private float timetolive = 10f;
    public item item;
    public int count = 1;

    private void Awake()
    {
        player = gamemanager.instance.player.transform;
    }

    public void set(item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    private void Update()
    {
        timetolive -= Time.deltaTime;
        if (timetolive < 0)
        {
            Destroy(gameObject);
        }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickupdistance)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position,
            player.position, speed * Time.deltaTime);
        if (distance <0.1f)
        {
            if (gamemanager.instance.inventorycontainer != null)
            {
                gamemanager.instance.inventorycontainer.add(item, count);
            }
            else
            {
                Debug.LogWarning("inventory container not attached to the game manager");
            }
            Destroy(gameObject);
        }
    }
}