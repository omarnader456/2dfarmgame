
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Tilemaps;

public class toolscharactercontroller : MonoBehaviour
{
    private Animator _animator;
    private charactercontroller2d _character2d;
    private character _character;
    private Rigidbody2D rigidbody;
    private toolbarcontroller _toolbarcontroller;
    [SerializeField] private float offsetdistance = 1f;
    [SerializeField] private float interactablearea = 8f;
    [SerializeField] private markermanager _markermanager;
    [SerializeField] private tilemapreadcontroller tilemapreadercontroller;
    Vector3Int selectedtileposition;
    bool selectable;
    [SerializeField] float maxdistance = 1.5f;
    [SerializeField]  toolaction ontilepickup;
    [SerializeField] private iconhighlight _iconhighlight;
    private attackcontroller _attackcontroller;
    [SerializeField] private int weaponenergycost = 5;
    [SerializeField] private float timeout = 1f;
    private float timer;
    characterlevel _characterlevel;
    
    void Awake()
    {
        _character = GetComponent<character>();
        _character2d = GetComponent<charactercontroller2d>();
        rigidbody = GetComponent<Rigidbody2D>();
        _toolbarcontroller = GetComponent<toolbarcontroller>();
        _animator = GetComponent<Animator>();
        _attackcontroller =  GetComponent<attackcontroller>();
        _characterlevel = GetComponent<characterlevel>();
    }
    
    private bool Usetoolworld()
    {
        if (timer > 0)
        {
            return false;
        }
        Vector2 position = rigidbody.position + _character2d.lastmotionvector * offsetdistance;
        item _item = _toolbarcontroller.getitem;
        if (_item == null)
        {
            return false;
        }

        if (_item.onaction == null)
        {
            return false;
        }
        energycost(getenergycost(_item.onaction));
        _animator.SetTrigger("act");
        bool complete = _item.onaction.onapply(position);
        if (complete == true)
        {
            _characterlevel.addexp(_item.onaction._skilltype,_item.onaction.skillexpreward);
            if (_item._onitemused != null)
            {
                _item._onitemused.onitemused(_item, gamemanager.instance.inventorycontainer);
            }
        }
        timer = timeout;
        return complete;
    }

    public int getenergycost(toolaction _action)
    {
        int _energycost = _action.energycost;
        _energycost -= _characterlevel.getlevel(_action._skilltype);
        if (_energycost < 1)
        {
            _energycost = 1;
        }
        return _energycost;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        selecttile();
        canselectcheck();
        marker();
        if (Input.GetMouseButtonDown(0))
        {
            weaponaction();
            if (Usetoolworld() == true)
            {
                return;
            }
            usetoolgrid();
        }
    }

    public void weaponaction()
    {
        if (timer > 0)
        {
            return;
        }
        item _item = _toolbarcontroller.getitem;
        if (_item == null)
        {
            return;
        }

        if (_item.isweapon == false)
        {
            return;
        }

        
        energycost(weaponenergycost);
        _attackcontroller.attack(_item.damage, _character2d.lastmotionvector);
        timer = timeout;
    }

    private void energycost(int cost)
    {
        item _item =  _toolbarcontroller.getitem;
        _character.gettired(cost);
    }

    private void selecttile()
    {
        selectedtileposition = tilemapreadercontroller.getgridposition(Input.mousePosition, true);
        selectedtileposition.z = 0;
    }

    void canselectcheck()
    {
        Vector2 characterposition = transform.position;
        Vector2 cameraposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterposition, cameraposition) < maxdistance;
        _markermanager.show(selectable);
        _iconhighlight.Canselect = selectable;
    }
    public void marker()
    {
        _markermanager.markedcellposition = selectedtileposition;
        _iconhighlight.cellposition= selectedtileposition;
    }

    private void usetoolgrid()
    {
        if (timer > 0)
        {
            return;
        }
        if (selectable == true)
        {
            item _item = _toolbarcontroller.getitem;
            if (_item == null)
            {
                pickuptile();
                return;
            }

            if (_item.ontilemapaction == null)
            {
                return;
            }
            energycost(getenergycost(_item.ontilemapaction));
            _animator.SetTrigger("act");
            bool complete = _item.ontilemapaction.onapplytotilemap(selectedtileposition, tilemapreadercontroller, _item);
            if (complete == true)
            {
                _characterlevel.addexp(_item.ontilemapaction._skilltype,_item.ontilemapaction.skillexpreward);
                if (_item._onitemused != null)
                {
                    _item._onitemused.onitemused(_item, gamemanager.instance.inventorycontainer);
                    _characterlevel.addexp(_item._onitemused._skilltype,100);
                }
            }
        } 
        timer = timeout;
    }

    private void pickuptile()
    {
        if (ontilepickup == null)
        {
            return;
        }

        ontilepickup.onapplytotilemap(selectedtileposition, tilemapreadercontroller, null);
    }
}
