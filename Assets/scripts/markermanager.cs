using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class markermanager : MonoBehaviour
{
    [SerializeField]  Tilemap targettilemap;
    [SerializeField] TileBase tile;
    public Vector3Int markedcellposition;
    private Vector3Int oldcellposition;
    bool shw;

    private void Update()
    {
        if (shw ==false){return;}
            targettilemap.SetTile(oldcellposition,null);
            markedcellposition.z = 0;
            //Debug.Log("markedcellposition: "+markedcellposition);
            targettilemap.SetTile(markedcellposition, tile);
            oldcellposition = markedcellposition;
    }

    internal void show(bool selectable)
    {
        shw = selectable;
        targettilemap.gameObject.SetActive(shw);
    }
}
