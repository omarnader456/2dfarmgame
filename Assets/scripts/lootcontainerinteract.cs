using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] 
public class lootcontainerinteract : interactable, ipersistant
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool open;
    [SerializeField] private AudioClip onopenaudio;
    [SerializeField] private AudioClip oncloseaudio;
    [SerializeField] private itemcontainer _itemcontainer;

    private void Awake()
    {
        if (_itemcontainer != null) {
            _itemcontainer = Instantiate(_itemcontainer);
            if (_itemcontainer.slots == null || _itemcontainer.slots.Count == 0) {
                _itemcontainer.init();
            }
        } else {
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
        Debug.Log($"5. chest '{gameObject.name}' received the interact signal!");
        
        if (open == false)
        {
            Debug.Log("6. chest thinks it is closed. ");
            _open(_character);
        }
        else
        {
            Debug.Log("6. chest thinks it is open.");
            _close(_character); 
        }
    }

    public void _open(character _character)
    {
        open = true;
        
        if (closed != null) closed.SetActive(false);
        if (opened != null) opened.SetActive(true);
       Debug.Log("chest open method"); 
        if (audiomanager.instance != null && onopenaudio != null) 
        {
            Debug.Log("chest audio");
            audiomanager.instance.play(onopenaudio);
        }
        
        var interactController = _character.GetComponent<itemcontainerinteractcontroller>();
        if (interactController != null) 
        {
            interactController.open(_itemcontainer, transform); 
        } 
        else 
        {
            Debug.LogError($"  player {_character.gameObject.name}  missing itemcontainerinteractcontroller component");
        }
    }

    public void _close(character _character)
    {
        open = false;
        
        if (closed != null) closed.SetActive(true);
        if (opened != null) opened.SetActive(false);
        
        if (audiomanager.instance != null && oncloseaudio != null) 
        {
            audiomanager.instance.play(oncloseaudio);
        }
        
        var interactController = _character.GetComponent<itemcontainerinteractcontroller>();
        if (interactController != null) 
        {
            interactController.close(); 
        }
    }

    [Serializable]
    public class savedlootitemdata {
        public string itemName;
        public int count;
        public savedlootitemdata(string name, int cnt) { itemName = name; count = cnt; }
    }

    [Serializable]
    public class tosave {
        public List<savedlootitemdata> itemdata = new List<savedlootitemdata>();
    }
    
    public string read()
    {
        tosave _tosave = new tosave();
        if (_itemcontainer != null && _itemcontainer.slots != null) {
            for (int i = 0; i < _itemcontainer.slots.Count; i++) {
                if (_itemcontainer.slots[i].itm == null) {
                    _tosave.itemdata.Add(new savedlootitemdata("", 0));
                } else {
                    _tosave.itemdata.Add(new savedlootitemdata(_itemcontainer.slots[i].itm.name, _itemcontainer.slots[i].count));
                }
            }
        }
        return JsonUtility.ToJson(_tosave);
    }

    public void load(string jsonstring)
    {
        if (_itemcontainer == null) init();
        if (string.IsNullOrEmpty(jsonstring) || jsonstring == "{}") return;
        
        tosave toload = JsonUtility.FromJson<tosave>(jsonstring);
        if (toload == null || toload.itemdata == null) return;

        while (_itemcontainer.slots.Count < toload.itemdata.Count) {
            _itemcontainer.slots.Add(new itemslot());
        }

        for (int i = 0; i < toload.itemdata.Count; i++) {
            if (string.IsNullOrEmpty(toload.itemdata[i].itemName)) {
                _itemcontainer.slots[i].clear();
            } else {
                if (gamemanager.instance != null && gamemanager.instance.itemdb != null) {
                    item foundItem = gamemanager.instance.itemdb.items.Find(x => x.name == toload.itemdata[i].itemName);
                    if (foundItem != null) {
                        _itemcontainer.slots[i].itm = foundItem;
                        _itemcontainer.slots[i].count = toload.itemdata[i].count;
                    } else {
                        _itemcontainer.slots[i].clear();
                    }
                }
            }
        }
    }
}