using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class lootcontainerinteract : interactable, ipersistant
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool open;
    [SerializeField] private AudioClip onopenaudio;
    [SerializeField] private AudioClip oncloseaudio;
    [SerializeField] private itemcontainer _itemcontainer;

    private void Start()
    {
        if (_itemcontainer == null)
        {
            init();
        }
    }

    private void init()
    {
            _itemcontainer = ScriptableObject.CreateInstance<itemcontainer>();
            _itemcontainer.init();
    }

    public override void interact(character _character)
    {
        if (open == false)
        {
            _open(_character);
        }
        else
        {
           _close(_character); 
        }
    }

    public void _open(character _character)
    {
        open = true;
        closed.SetActive(false);
        opened.SetActive(true);
            
        audiomanager.instance.play(onopenaudio);
        _character.GetComponent<itemcontainerinteractcontroller>().open(_itemcontainer, transform); 
    }

    public void _close(character _character)
    {
        open = false;
        closed.SetActive(true);
        opened.SetActive(false);
        audiomanager.instance.play(oncloseaudio);
        _character.GetComponent<itemcontainerinteractcontroller>().close(); 
    }

    [Serializable]
    public class savedlootitemdata
    {
        public int itemid;
        public int count;

        public savedlootitemdata(int id, int cnt)
        {
            itemid = id;
            count = cnt;
        }
    }
    [Serializable]
    public class tosave
    {
        public List<savedlootitemdata> itemdata;
        public  tosave()
        {
            itemdata = new List<savedlootitemdata>();
        }
    }
    
    public string read()
    {
        tosave _tosave = new tosave();
        for (int i = 0; i < _itemcontainer.slots.Count; i++)
        {
            if (_itemcontainer.slots[i].itm == null)
            {
                _tosave.itemdata.Add(new savedlootitemdata(-1,0));
            }
            else
            {
                _tosave.itemdata.Add(new savedlootitemdata(_itemcontainer.slots[i].itm.id,
                    _itemcontainer.slots[i].count));
            }
        }
        return JsonUtility.ToJson(_tosave);
    }

    public void load(string jsonstring)
    {
        if (jsonstring == "" || jsonstring == "{}" || jsonstring == null)
        {
            return;
        }

        if (_itemcontainer == null)
        {
            init();
        }
        tosave toload = JsonUtility.FromJson<tosave>(jsonstring);
        for (int i = 0; i < toload.itemdata.Count; i++)
        {
            if (toload.itemdata[i].itemid == -1)
            {
                _itemcontainer.slots[i].clear();
            }
            else
            {
                _itemcontainer.slots[i].itm = gamemanager.instance.itemdb.items[toload.itemdata[i].itemid];
                _itemcontainer.slots[i].count = toload.itemdata[i].count;
            }
        }
    }
}
