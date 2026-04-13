using System;
using UnityEngine;

public class itemcontainerinteractcontroller : MonoBehaviour
{
    private itemcontainer targetitemcontainer;
    [SerializeField] itemcontainerpanel _itemcontainerpanel;
    [SerializeField] inventorycontroller _inventorycontroller;
    Transform openchest;
    [SerializeField]  float maxdistance = 3f;
   
    private void Awake()
    {
        _inventorycontroller = GetComponent<inventorycontroller>();
    }

    private void Update()
    {
        if (openchest != null)
        {
            float distance = Vector2.Distance(openchest.position, transform.position);
            if (distance > maxdistance)
            {
                openchest.GetComponent<lootcontainerinteract>()._close(GetComponent<character>());
            }
        } 
    }

    public void open(itemcontainer _itemcontainer, Transform _openchest)
    {
        targetitemcontainer = _itemcontainer;
        _itemcontainerpanel.inventory = targetitemcontainer;
        _inventorycontroller.open();
        _itemcontainerpanel.gameObject.SetActive(true);
        openchest = _openchest;
    }

    public void close()
    {
        _inventorycontroller.close();
        _itemcontainerpanel.gameObject.SetActive(false);
        openchest = null;
    }
}