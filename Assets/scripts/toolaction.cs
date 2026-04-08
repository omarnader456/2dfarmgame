using UnityEngine;

public class toolaction : ScriptableObject
{
    public typeofskill _skilltype;
    public int skillexpreward = 100;
    public int energycost = 0;
    public virtual bool onapply(Vector2 worldpoint)
    {
        Debug.Log("onapply is not implemented");
        return true;
    }

    public virtual bool onapplytotilemap(Vector3Int gridposition, tilemapreadcontroller _tilemapreadcontroller, item _item)
    {
        Debug.Log("onapplytilemap not implemented");
        return true;
    }

    public virtual void onitemused(item useditem, itemcontainer inventory)
    {
        
    }

}
