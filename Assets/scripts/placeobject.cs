using UnityEngine;
[CreateAssetMenu(menuName = "Data/toolaction/placeobject")]
public class placeobject :toolaction 
{
    public override bool onapplytotilemap(Vector3Int gridposition, tilemapreadcontroller _tilemapreadcontroller, item _item)
    {
        if (_tilemapreadcontroller.objectmanager.check(gridposition) == true)
        {
            return false;
        }
        _tilemapreadcontroller.objectmanager.place(_item, gridposition);
       return true; 
    }

    public override void onitemused(item useditem, itemcontainer inventory)
    {
        inventory.remove(useditem);
    }
}
