using UnityEngine;
using UnityEngine.Tilemaps;

public class tilemapcropsmanager : timeagent
{
   [SerializeField]  cropscontainer container;
   [SerializeField]  TileBase plowed;
   [SerializeField]  TileBase seeded;
   public Tilemap targettilemap;
   [SerializeField]  GameObject cropspriteprefab;


   public Tilemap Targettilemap
   {
       get
       {
           if (targettilemap == null)
           {
               GameObject obj = GameObject.Find("cropstilemap");
               if (obj != null)
               {
                   targettilemap = obj.GetComponent<Tilemap>();
               }
           }
           return targettilemap;
       }
   }

   private void OnDestroy()
   {
       for (int i = 0; i < container.crops.Count; i++)
       {
           container.crops[i].renderer = null;
       }
   }
   internal bool check(Vector3Int position)
   {
       return container.get(position) != null;
   }
   private void Start()
   {
       gamemanager.instance.GetComponent<cropsmanager>()._cropsmanager = this;
       init();
       ontimetick += tick;
       visualizemap();
   }
   
   

   private void visualizemap()
   {
       for (int i = 0; i < container.crops.Count; i++)
       {
           visualizetile(container.crops[i]);
       }
   }

   public void RefreshTilemap()
   {
       targettilemap = null;
   }
   
   public void tick(daytimecontroller _daytimecontroller)
   {
      if (Targettilemap == null)
      {
         Debug.Log("tick() tilemap is null");
         return;
      }
      foreach (croptile _croptile in container.crops)
      {
         if (_croptile._crop == null)
         {
            continue;
         }

         _croptile.damage += 0.02f;

         if (_croptile.damage > 1f)
         {
            _croptile.harvested();
            targettilemap.SetTile((Vector3Int)_croptile.position, plowed);
            continue;
         }
         if (_croptile.complete)
         {
            Debug.Log("crop done growing");
            {
                visualizetile(_croptile);
            }
            continue;
         }
         _croptile.growtimer += 1;
         if (_croptile.growtimer >= _croptile._crop.growthstagetime[_croptile.growstage])
         {
            _croptile.growstage += 1;
            visualizetile(_croptile);
         }
            
      }
   }
    

    public void seed(Vector3Int position, crop toseed)
    {
       croptile tile = container.get(position);
       if (tile == null)
       {
           return;
       }
       if (tile._crop != null)
       {
           return;
       }
        targettilemap.SetTile(position, seeded);
        tile._crop = toseed;
    }

    public void plow(Vector3Int position)
    {
        if (check(position) == true)
        {
            return;
        }
        createplowedtile(position);
    }

    private void createplowedtile(Vector3Int position)
    {
        if (Targettilemap == null)
        {
            Debug.Log("createplowtile() tilemap is null");

            return;
        }
        croptile crop = new croptile();
        container.Add(crop);
        
        crop.position = position;
        visualizetile(crop);
        targettilemap.SetTile((Vector3Int)position, plowed);
    }

    internal void pickup(Vector3Int gridposition)
    {
        croptile tile = container.get(gridposition); 
        Vector2Int position = (Vector2Int)gridposition;
        if (tile == null)
        {
            return;
        }
        if (tile.complete)
        {
            itemspawnmanager.instance.spawnitem(targettilemap.CellToWorld(gridposition),
                tile._crop.yield, tile._crop.count);
            tile.harvested();
            visualizetile(tile);
        }
    }

    public void visualizetile(croptile _croptile)
    {
        if (Targettilemap == null)
        {
            return;
        }
        targettilemap.SetTile(_croptile.position, _croptile._crop != null? seeded: plowed);
        if (_croptile.renderer == null)
        {
            GameObject obj = Instantiate(cropspriteprefab, transform);
            obj.transform.position = targettilemap.CellToWorld(_croptile.position);
            obj.transform.position -= Vector3.forward * 0.01f;
            _croptile.renderer = obj.GetComponent<SpriteRenderer>(); 
        }
        bool growing = _croptile._crop != null;
        _croptile.renderer.gameObject.SetActive(growing);
        if (growing)
        {
            int stage = _croptile.growstage;
            if (stage >= _croptile._crop.sprite.Count)
            {
                stage = _croptile._crop.sprite.Count - 1;
            }
            _croptile.renderer.sprite = _croptile._crop.sprite[stage];
        }
    }
}
