using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class iconhighlight : MonoBehaviour
{
   public Vector3Int cellposition;
   Vector3 targetposition;
   [SerializeField]  Tilemap targettilemap;
   private SpriteRenderer _spriteRenderer;
   private bool canselect;
   private bool _show;

   public bool Canselect
   {
       set
       {
           canselect = value;
           gameObject.SetActive(canselect && _show);
       }
   }

   public bool Show
   {
       set
       {
           _show = value;
           gameObject.SetActive(canselect && _show);
           Debug.Log("Show:" + _show);
       }
   }
   private void Update()
   {
       targetposition = targettilemap.CellToWorld(cellposition);
       transform.position = targetposition + targettilemap.cellSize/2;
       Debug.Log("transform.position:" + transform.position.ToString());
   }

   

   internal void set(Sprite icon)
   {
       if (_spriteRenderer == null)
       {
           _spriteRenderer = GetComponent<SpriteRenderer>();
       }
       _spriteRenderer.sprite = icon;
   }
}
