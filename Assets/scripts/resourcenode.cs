using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class resourcenode : toolhit
{
    [Header("Main Drop")]
    [SerializeField] private GameObject pickupdrop;
    [SerializeField] private int dropcount = 5;
    [SerializeField] private float spread = 0.7f;
    [SerializeField] private item item;
    [SerializeField] private int itemcountinonedrop = 1;
    [SerializeField] private resourcenodetype nodetype;

    [Header("Rare/Secondary Drop")]
    [SerializeField] private item secondaryItem;
    [SerializeField] [Range(0f, 1f)] private float secondaryDropChance = 0.2f; 
    [SerializeField] private int secondaryItemCount = 1;

    override public void hit()
    {
        while (dropcount > 0)
        {
            dropcount -= 1;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            
            itemspawnmanager.instance.spawnitem(position, item, itemcountinonedrop);

            if (secondaryItem != null)
            {
                
                if (UnityEngine.Random.value <= secondaryDropChance)
                {
                    Vector3 secondaryPos = position;
                    secondaryPos.x += spread * UnityEngine.Random.value - spread / 2;
                    itemspawnmanager.instance.spawnitem(secondaryPos, secondaryItem, secondaryItemCount);
                }
            }
        }
        
        spawnedobject _gameobject = GetComponent<spawnedobject>();
        if (_gameobject != null)
        {
            _gameobject.spawnedobjectdestroy();
        }
        Destroy(gameObject);
    }

    public override bool canbehit(List<resourcenodetype> _canbehit)
    {
        return _canbehit.Contains(nodetype);
    }
}