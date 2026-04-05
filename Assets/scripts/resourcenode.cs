using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class resourcenode : toolhit
{
    [SerializeField] private GameObject pickupdrop;
    [SerializeField] private int dropcount = 5;
    [SerializeField] private float spread = 0.7f;
    [SerializeField] private item item;
    [SerializeField] private int itemcountinonedrop=1;
    [SerializeField] private resourcenodetype nodetype;
    override public void hit()
    {
        while (dropcount > 0)
        {
            dropcount -= 1;
            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            itemspawnmanager.instance.spawnitem(position, item, itemcountinonedrop);
        }

        Destroy(gameObject);
    }

    public override bool canbehit(List<resourcenodetype> _canbehit)
    {
        return _canbehit.Contains(nodetype);
    }
}
