using System;
using System.Collections.Generic;
using UnityEngine;

public class itempanel : MonoBehaviour
{
    public itemcontainer inventory;
    public  List<inventorybutton> buttons;
    private void Start()
    {
        init();
    }

    private void LateUpdate()
    {
        if (inventory == null)
        {
            return;
        } 
        
    }

    public void init()
    {
        setsourcepanel();
        setindex();
        show();
    }

    private void setsourcepanel()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].setitempanel(this);
        }
    }

    private void setindex()
    {
        for (int i = 0;  i < buttons.Count; i++)
        {
            buttons[i].setindex(i);
        }
    }

    private void OnEnable()
    {
        clear();
        inventory.change += show;
        show();
    }

    private void OnDisable()
    {
        inventory.change -= show;
    }

    public void setinventory(itemcontainer newinventory)
    {
        inventory = newinventory;
    }

    public virtual void show()
    {
        if (inventory == null)
        {
            return;
        }
        for (int i = 0; i < inventory.slots.Count && i < buttons.Count; i++)
        {
            if (inventory.slots[i].itm == null)
            {
                buttons[i].clean();
            }
            else
            {
                buttons[i].set(inventory.slots[i]);
            }
        }
    }

    public virtual void onclick(int id)
    {
        
    }

    public void clear()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].clean();
        }
    }
}
