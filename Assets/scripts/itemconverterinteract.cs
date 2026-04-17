using System;
using UnityEngine;

[Serializable]
public class itemconvertordata_save
{
    public string item_name;
    public int count;
    public int timer;
}

[Serializable]
public class itemconvertordata
{
    public itemslot _itemslot;
    public int timer;

    public itemconvertordata()
    {
        _itemslot = new itemslot();
    }
}

[RequireComponent(typeof(timeagent))]
public class itemconverterinteract : interactable, ipersistant
{
    [SerializeField] private item convertableitem;
    [SerializeField] private item produceditem;

    [SerializeField] int produceditemcount = 1;
    [SerializeField] private int timetoprocess = 5;
    Animator animator;
    private itemconvertordata data;

    private void Start()
    {
        timeagent _timeagent = GetComponent<timeagent>();
        _timeagent.ontimetick += itemconvertprocess;
        if (data == null)
        {
            data = new itemconvertordata();
        }
        animator = GetComponent<Animator>();
        animate();
    }

    private void itemconvertprocess(daytimecontroller _daytimecontroller)
    {
        if (data == null || data._itemslot == null) return;

        if (data.timer > 0)
        {
            data.timer -= 1;
            if (data.timer <= 0)
            {
                completeitemconversion();
            }
        } 
    }

    public override void interact(character character)
    {
        if (data == null) return;

        if (data._itemslot.itm == null)
        {
            if (gamemanager.instance.draganddropcontroller.check(convertableitem))
            {
                startitemprocessing(gamemanager.instance.draganddropcontroller.itemslot);
                return;
            }
            toolbarcontroller _toolbarcontroller = character.GetComponent<toolbarcontroller>();
            if (_toolbarcontroller == null) return;

            itemslot _itemslot = _toolbarcontroller.getitemslot;
            if (_itemslot.itm == convertableitem)
            {
                startitemprocessing(_itemslot);
                return;
            }
        }

        if (data._itemslot.itm != null && data.timer <= 0f)
        {
            gamemanager.instance.inventorycontainer.add(data._itemslot.itm, data._itemslot.count);
            data._itemslot.clear();
            animate(); 
        } 
    }

    private void startitemprocessing(itemslot toprocess)
    {
        data._itemslot.copy(gamemanager.instance.draganddropcontroller.itemslot);
        data._itemslot.count = 1;
        if (toprocess.itm.stackable)
        {
            toprocess.count -= 1;
            if (toprocess.count <= 0)
            {
                toprocess.clear();
            }
        }
        else
        {
            toprocess.clear();
        }
        data.timer = timetoprocess;
        animate();
    }

    private void animate()
    {
        if (animator != null && data != null)
        {
            animator.SetBool("working", data.timer > 0f);
        }
    }

    private void completeitemconversion()
    {
        data._itemslot.clear();
        data._itemslot.set(produceditem, produceditemcount);
        animate();
    }

    public string read()
    {
        if (data == null) data = new itemconvertordata();

        itemconvertordata_save savedata = new itemconvertordata_save();
        savedata.timer = data.timer;
        
        if (data._itemslot != null && data._itemslot.itm != null)
        {
            savedata.item_name = data._itemslot.itm.name;
            savedata.count = data._itemslot.count;
        }
        else
        {
            savedata.item_name = "";
            savedata.count = 0;
        }
        
        return JsonUtility.ToJson(savedata);
    }

    public void load(string jsonstring)
    {
        if (string.IsNullOrEmpty(jsonstring) || jsonstring == "{}") 
        {
            if (data == null) data = new itemconvertordata();
            return;
        }

        itemconvertordata_save savedata = JsonUtility.FromJson<itemconvertordata_save>(jsonstring);
        if (data == null) data = new itemconvertordata();

        data.timer = savedata.timer;

        if (!string.IsNullOrEmpty(savedata.item_name))
        {
            item foundItem = gamemanager.instance.itemdb.items.Find(x => x.name == savedata.item_name);
            if (foundItem != null)
            {
                data._itemslot.set(foundItem, savedata.count);
            }
            else
            {
                data._itemslot.clear();
            }
        }
        else
        {
            data._itemslot.clear();
        }

        if (animator != null) animate(); 
    }
}