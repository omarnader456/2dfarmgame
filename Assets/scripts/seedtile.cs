using UnityEngine;
[CreateAssetMenu(menuName = "Data/toolaction/seedtile")]
public class seedtile : toolaction
{
    public override bool onapplytotilemap(Vector3Int gridposition, tilemapreadcontroller _tilemapreadcontroller, item _item)
    {
        if (_tilemapreadcontroller._cropsmanager.check(gridposition) == false)
        {
            return false;
        }
        _tilemapreadcontroller._cropsmanager.seed(gridposition, _item._crop);
        return true;
    }

    public override void onitemused(item useditem, itemcontainer inventory)
    {
        inventory.remove(useditem);
    }
}
