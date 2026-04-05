using UnityEngine;
[CreateAssetMenu(menuName = "Data/toolaction/harvest")]
public class tilepickupaction : toolaction 
{
    public override bool onapplytotilemap(Vector3Int gridposition, tilemapreadcontroller _tilemapreadcontroller, item _item)
    {
        _tilemapreadcontroller._cropsmanager.pickup(gridposition);
        _tilemapreadcontroller.objectmanager.pickup(gridposition);
        return true;
    }
}
