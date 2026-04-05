using System;
using System.ComponentModel.Design;
using UnityEngine;

[Serializable]
public class stat
{
    public int maxval;
    public int currval;
    

    public stat(int curr, int max)
    {
        currval = curr;
        maxval = max;
    }

    public void subtract(int amount)
    {
        currval -= amount;
    }

    public void add(int amount)
    {
        currval += amount;
        currval = currval > maxval ? maxval : currval;
    }

    public void settomax()
    {
        currval = maxval;
    }
}

public class character : MonoBehaviour, idamageable
{
    public stat hp;
    public stat stamina;
    public bool isdead;
    public bool isexhausted;
    [SerializeField] private statusbar staminabar;
    [SerializeField] private statusbar hpbar;
    disablecontrols __disablecontrols;
    playerrespawn _playerrespawn;


    private void Awake()
    {
       __disablecontrols = GetComponent<disablecontrols>(); 
       _playerrespawn = GetComponent<playerrespawn>();
       Debug.Log("character awake");
       Debug.Log("playerrespawn " + _playerrespawn);
       Debug.Log("disablecontrols " + __disablecontrols);
    }

    private void Start()
    {
        updatehpbar();
        updatestaminabar();
    }

    private void updatestaminabar()
    {
        staminabar.set(stamina.currval, stamina.maxval);
    }

    public void takedamage(int amount)
    {
        Debug.Log("takedamage in character");
        hp.subtract(amount);
        if (hp.currval <= 0)
        {
            Debug.Log("hp less or equal to zero");
            dead();
        }

        updatehpbar();
    }

    private void dead()
    {
        isdead = true;
        __disablecontrols.disablecontrol();
        Debug.Log("dead function");
        Debug.Log("playerrespawn "+_playerrespawn);
        _playerrespawn.startrespawn();
    }


    private void updatehpbar()
    {
        hpbar.set(hp.currval, hp.maxval);
    }

    public void heal(int amount)
    {
        hp.add(amount);
        updatehpbar();
    }
    public void fullheal()
    {
        hp.settomax();
        updatehpbar();
    }

    public void gettired(int amount)
    {
        stamina.subtract(amount);
        if (stamina.currval <= 0)
        {
            exhausted();
        }
        updatestaminabar();
    }

    private void exhausted()
    {
        isexhausted = true;
        __disablecontrols.disablecontrol();
        _playerrespawn.startrespawn();
    }

    public void rest(int amount)
    {
        stamina.add(amount);
        updatestaminabar();
    }
    public void fullrest()
    {
        stamina.settomax();
        updatestaminabar();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            takedamage(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gettired(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            rest(10);
        }
    }

    public void calculatedamage(ref int damage)
    {
    }

    public void applydamage(int damage)
    {
        takedamage(damage);
    }

    public void checkstate()
    {
    }
}
