using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class itemdraganddropcontroller : MonoBehaviour
{
    public  itemslot itemslot;
    [SerializeField] private GameObject itemicon;
    RectTransform icontransform;
    Image itemiconimage;
    
    private void Start()
    {
        itemslot = new itemslot();
        icontransform = itemicon.GetComponent<RectTransform>();
        itemiconimage = itemicon.GetComponent<Image>();
    }

    private void Update()
    {
        if (itemicon.activeInHierarchy == true)
        {
            icontransform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject() == false)
                {
                    Vector3 worldposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    worldposition.z = 0;
                    itemspawnmanager.instance.spawnitem(worldposition,
                        itemslot.itm,
                        itemslot.count);
                    itemslot.clear();
                    itemicon.SetActive(false);
                }
            }
        }
    }

    public void onclick(itemslot itemslot)
    {
        if (this.itemslot.itm == null)
        {
            this.itemslot.copy(itemslot);
            itemslot.clear();
        }
        else
        {
            if (itemslot.itm == this.itemslot.itm)
            {
                itemslot.count +=this.itemslot.count;
                this.itemslot.clear();
            }
            else
            {
                item item = itemslot.itm;
                int count = itemslot.count;

                itemslot.copy(this.itemslot);
                this.itemslot.set(item, count);
            }
        }
        updateicon();
    }

    public void updateicon()
    {
        if (itemslot.itm == null)
        {
            itemicon.SetActive(false);
        }
        else
        {
            itemicon.SetActive(true);
            itemiconimage.sprite = itemslot.itm.icon;
        }
    }

    public bool check(item _item, int count = 1)
    {
        if (itemslot == null)
        {
            return false;
        }
        if (_item.stackable)
        {
            return itemslot.itm == _item && itemslot.count >= count;
        }

        return itemslot.itm == _item;
    }

    internal void removeitem(int count = 1)
    {
        if (itemslot == null)
        {
            return;
        }
        if (itemslot.itm.stackable)
        {
            itemslot.count -= count;
            if (itemslot.count <= 0)
            {
                itemslot.clear();
            }

            else
            {
                itemslot.clear();
            }
            updateicon();
        }
    }

    public bool checkforsale()
    {
        if (itemslot.itm == null)
        {
            return false;
        }
        bool result = itemslot.itm.canbesold == true ? true : false;
        return result;
    }
}
