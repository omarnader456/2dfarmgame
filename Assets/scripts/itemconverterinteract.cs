using System;
using System.Reflection;
using UnityEngine;
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
        if (data._itemslot == null)
        {
            return;
        }

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
        if (data._itemslot.itm == null)
        {
            if (gamemanager.instance.draganddropcontroller.check(convertableitem))
            {
                startitemprocessing(gamemanager.instance.draganddropcontroller.itemslot);
                return;
            }
            toolbarcontroller _toolbarcontroller = character.GetComponent<toolbarcontroller>();
            if (_toolbarcontroller == null)
            {
                return;
            }

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
        animator.SetBool("working", data.timer > 0f);
    }

   

    private void completeitemconversion()
    {
        animate();
        data._itemslot.clear();
        data._itemslot.set(produceditem, produceditemcount);
    }

    public string read()
    {
        return JsonUtility.ToJson(data);
    }

    public void load(string jsonstring)
    {
        data = JsonUtility.FromJson<itemconvertordata>(jsonstring);
    }
}