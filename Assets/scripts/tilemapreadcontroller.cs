using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapreadcontroller : MonoBehaviour
{
    [SerializeField]  Tilemap tilemap;
    
    public cropsmanager _cropsmanager;
    public placeableobjectsreferencemanager objectmanager;

    public Vector3Int getgridposition(Vector2 position, bool mouseposition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        }

        if (tilemap == null)
        {
            return Vector3Int.zero;
        }
        Vector3 worldposition;
        if (mouseposition)
        {
            worldposition = Camera.main.ScreenToWorldPoint(position);
            worldposition.z = 0;
        }
        else
        {
            worldposition = position;
        }
        Vector3Int gridposition = tilemap.WorldToCell(worldposition);
        gridposition.z = 0;
        return gridposition;
    }

    public TileBase gettilebase(Vector3Int gridposition)
    {
        if (tilemap == null)
        {
            tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        }

        if (tilemap == null)
        {
            return null;
        }
        TileBase tile = tilemap.GetTile(gridposition);
        return tile;
    }
    public void RefreshTilemap()
    {
      tilemap = null; 
    }
    
}
