using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/toolaction/plow")]
public class plowtile : toolaction
{
    [SerializeField] List<TileBase> canplow;
    [SerializeField] private AudioClip onplowused;
    public override bool onapplytotilemap(Vector3Int gridposition, tilemapreadcontroller _tilemapreadcontroller, item _item)
    {
        TileBase tiletoplow = _tilemapreadcontroller.gettilebase(gridposition);
        if (canplow.Contains(tiletoplow) == false)
        {
            return false;
        }
        _tilemapreadcontroller._cropsmanager.plow(gridposition); 
        audiomanager.instance.play(onplowused);
        return true;
    }
}
