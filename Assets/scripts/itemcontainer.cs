using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class itemslot
{
    public item itm;
    public int count;

    public void copy(itemslot slot)
    {
        itm = slot.itm;
        count = slot.count;
    }

    public void set(item item, int count)
    {
        this.itm = item;
        this.count = count;
    }

    public void clear()
    {
        itm = null;
        count = 0;
    }
    
}

[CreateAssetMenu(fileName = "itemcontainer", menuName = "Data/itemcontainer")]
public class itemcontainer : ScriptableObject
{
    public List<itemslot> slots;
    public Action change;

    public void add(item i, int count = 1)
    {
        if (i.stackable == true)
        {
            itemslot itemslot = slots.Find(x => x.itm == i);
            if (itemslot != null)
            {
                itemslot.count += count;
            }
            else
            {
                itemslot = slots.Find(x => x.itm == null);
                if (itemslot != null)
                {
                    itemslot.itm = i;
                    itemslot.count = count;
                }
            }
        }
        else
        {
            itemslot itemslot = slots.Find(x => x.itm == null);
            if (itemslot != null)
            {
                itemslot.itm = i;
            }
        }
        change?.Invoke();
    }

    public void remove(item itemtoremove, int count = 1)
    {
        if (itemtoremove.stackable == true)
        {
            itemslot _itemslot = slots.Find(x => x.itm == itemtoremove);
            if (_itemslot == null)
            {
                return;
            }
            _itemslot.count -= count;
            if (_itemslot.count <= 0)
            {
                _itemslot.clear();
            }
        }
        else
            {
                while (count > 0)
                {
                    count -= 1;
                    itemslot _itemslot = slots.Find(x => x.itm ==itemtoremove);
                    if (_itemslot == null)
                    {
                        return;
                    }
                    _itemslot.clear();
                }
            }
        change?.Invoke();
    }

    internal bool checkfreespace()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itm == null)
            {
                return true;
            }
        }
        return false;
    }

    internal bool checkitem(itemslot checkingitem)
    {
       itemslot _itemslot= slots.Find(x => x.itm == checkingitem.itm);

       if (_itemslot == null)
       {
           return false;
       }

       if (checkingitem.itm.stackable)
       {
           return _itemslot.count > checkingitem.count;
       }
       
       return true;
    }

    internal void init()
    {
        slots = new List<itemslot>();
        for (int i = 0; i < 60; i++)
        {
            slots.Add(new itemslot());
        }
    }
}
