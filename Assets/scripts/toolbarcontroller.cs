using System;
using UnityEngine;

public class toolbarcontroller : MonoBehaviour
{
    [SerializeField] private int toolbarsize = 13;
    private int selectedtool;
    public Action<int> onchange;
    [SerializeField] iconhighlight _iconhighlight;

    public itemslot getitemslot
    {
        get
        {
            var slots = gamemanager.instance.inventorycontainer.slots;
            
            if (slots == null || slots.Count == 0 || selectedtool >= slots.Count)
            {
                return null;
            }
            return slots[selectedtool];
        }
    }

    public item getitem
    {
        get
        {
            itemslot slot = getitemslot;
            if (slot == null) return null;
            
            return slot.itm;
        }
    }

    private void Start()
    {
       onchange+= updatehighlighticon;
       updatehighlighticon(selectedtool);
    }
    

   

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (Input.mouseScrollDelta.y != 0)
        {
            if (delta > 0)
            {
                selectedtool += 1;
                selectedtool = (selectedtool >= toolbarsize ? 0 : selectedtool);
            }
            else
            {
                selectedtool -= 1;
                selectedtool = (selectedtool < 0 ? toolbarsize -1  : selectedtool);
            }
            onchange?.Invoke(selectedtool);
            Debug.Log($"{selectedtool.ToString()} selected");
        }
    }

    internal void set(int id)
    {
        selectedtool = id;
    }

   public  void updatehighlighticon(int id = 0)
    {
        item itm = getitem;
        if (itm == null)
        {
            Debug.Log($"{id.ToString()} item is null");
            _iconhighlight.Show = false;
            return;
        }
        Debug.Log($"{id.ToString()} item: {itm}");
        _iconhighlight.Show = itm.iconhighlight;
        if (itm.iconhighlight)
        {
            _iconhighlight.set(itm.icon);
        }
    }
}
