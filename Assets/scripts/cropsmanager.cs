using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class croptile
{
    public crop _crop;
    public int growtimer;
    public SpriteRenderer renderer;
    public int growstage;
    public float damage;
    public Vector3Int position; 

    public bool complete
    {
        get
        {
            if (_crop == null)
            {
               return  false;
                
            }
            return growtimer >= _crop.timetogrow;
        }
    }

    internal void harvested()
    {
        growtimer = 0;
        growstage = 0;
        _crop = null;
        renderer.gameObject.SetActive(false);
        damage = 0;
        if (renderer != null)
        {
            renderer.gameObject.SetActive(false);
        }
    }
}
public class cropsmanager : MonoBehaviour
{
    public tilemapcropsmanager _cropsmanager;
    public void pickup(Vector3Int position)
    {
        if (_cropsmanager == null)
        {
            Debug.LogWarning("no tilemap crops manager referenced in the crops manager");
            return;
        }
        _cropsmanager.pickup(position);
    }

    public bool check(Vector3Int position)
    {
        if (_cropsmanager == null)
        {
            Debug.LogWarning("no tilemap crops manager referenced in the crops manager");
            return false;
        }
        return _cropsmanager.check(position);
    }

    public void seed(Vector3Int position, crop toseed)
    {
        if (_cropsmanager == null)
        {
            Debug.LogWarning("no tilemap crops manager referenced in the crops manager");
            return;
        }
        _cropsmanager.seed(position, toseed);
    }

    public void plow(Vector3Int position)
    {
        if (_cropsmanager == null)
        {
            Debug.LogWarning("no tilemap crops manager referenced in the crops manager");
            return;
        }
        _cropsmanager.plow(position);
    }

    public void RefreshTilemap()
    {
        _cropsmanager.RefreshTilemap();
    }
}
