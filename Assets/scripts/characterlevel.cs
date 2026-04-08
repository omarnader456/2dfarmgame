using System;
using UnityEngine;

public enum typeofskill
{
    cutting,
    mining,
    fighting
}
[Serializable]
public class skill
{
    public int level = 1;
    public int experience = 0;

    public int nextlevel
    {
        get
        {
            return level * 1000;
        }
    }

    public typeofskill _skill;

    public skill(typeofskill _skill)
    {
        level = 1;
        experience = 0;
        this._skill = _skill;
    }

    public void addexp(int exp)
    {
        experience += exp;
        checklevelup();
    }

    private void checklevelup()
    {
        if (experience >= nextlevel)
        {
            experience -= nextlevel;
            level++;
        }
    }
}
public class characterlevel : MonoBehaviour
{
    [SerializeField] skill _cutting;
    [SerializeField] skill _mining;
    [SerializeField] skill _fighting;

    private void Start()
    {
        _cutting = new skill(typeofskill.cutting);
        _mining = new skill(typeofskill.mining);
        _fighting = new skill(typeofskill.fighting);
    }

    public int getlevel(typeofskill _skill)
    {
       skill variable = getskill(_skill);

       if (variable != null)
       {
           return variable.level;
       }
        return -1;
    }

    public void addexp(typeofskill _skill, int exp)
    {
        skill variable = getskill(_skill);
        variable.addexp(exp);
    }

    public skill getskill(typeofskill _skill)
    {
        switch (_skill)
        {
            case typeofskill.cutting:
                return _cutting;
            case typeofskill.mining:
                return _mining;
            case typeofskill.fighting:
                return  _fighting;
        } 
        return null;
    }
}
