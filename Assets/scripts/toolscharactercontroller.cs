
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
    
    void Awake()
    {
        _character = GetComponent<character>();
        _character2d = GetComponent<charactercontroller2d>();
        rigidbody = GetComponent<Rigidbody2D>();
        _toolbarcontroller = GetComponent<toolbarcontroller>();
        _animator = GetComponent<Animator>();
        _attackcontroller =  GetComponent<attackcontroller>();
    }
    
    private bool Usetoolworld()
    {
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
        energycost(_item.onaction.energycost);
        _animator.SetTrigger("act");
        bool complete = _item.onaction.onapply(position);
        if (complete == true)
        {
            if (_item._onitemused != null)
            {
                _item._onitemused.onitemused(_item, gamemanager.instance.inventorycontainer);
            }
        }
        return complete;
    }

    private void Update()
    {
        
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
            energycost(_item.ontilemapaction.energycost);
            _animator.SetTrigger("act");
            bool complete = _item.ontilemapaction.onapplytotilemap(selectedtileposition, tilemapreadercontroller, _item);
            if (complete == true)
            {
                if (_item._onitemused != null)
                {
                    _item._onitemused.onitemused(_item, gamemanager.instance.inventorycontainer);
                }
            }
        } 
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
