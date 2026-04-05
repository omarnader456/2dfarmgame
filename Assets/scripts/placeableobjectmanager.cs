using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class placeableobjectmanager : MonoBehaviour
{
   [SerializeField] private placeableobjectcontainer _placeableobjects;
   [SerializeField] private Tilemap targettilemap;

   private void OnDestroy()
   {
      for (int i = 0; i < _placeableobjects.placeableobjects.Count; i++)
      {
         if (_placeableobjects.placeableobjects[i].targetobject == null)
         {
            continue;
         } 
         ipersistant persistant = _placeableobjects.placeableobjects[i].targetobject.GetComponent<ipersistant>();
         Debug.Log(persistant + " in ondestroy");
         if (persistant != null)
         {
            string jsonstring = persistant.read();
            _placeableobjects.placeableobjects[i].objectstate = jsonstring;
         }
         _placeableobjects.placeableobjects[i].targetobject = null;
      } 
   }

  
   private void Start()
   {
      gamemanager.instance.GetComponent<placeableobjectsreferencemanager>()._placeableobjectmanager = this;
      visualizemap();
   }

   public void place(item _item, Vector3Int positionongrid)
   {
      if (check(positionongrid) == true)
      {
         return;
      }
      placeableobject _placeableobject = new placeableobject(_item,positionongrid);
     visualizeitem(_placeableobject);
     _placeableobjects.placeableobjects.Add(_placeableobject);
   }

   private void visualizemap()
   {
      for (int i = 0; i < _placeableobjects.placeableobjects.Count; i++)
      {
         visualizeitem(_placeableobjects.placeableobjects[i]);
      }
   }

   private void visualizeitem(placeableobject _placeableobject)
   {
      GameObject obj = Instantiate(_placeableobject.placeditem.itemprefab);
      obj.transform.parent = transform;
      Vector3 position = targettilemap.CellToWorld(_placeableobject.positionongrid) 
                         + targettilemap.cellSize / 2;
      obj.transform.position = position;
      ipersistant persistant = obj.GetComponent<ipersistant>();
      if (persistant != null)
      {
         persistant.load(_placeableobject.objectstate);
      }
      _placeableobject.targetobject = obj.transform;
      Debug.Log(obj.name + " has been placed at "+obj.transform.position);
   }

   public bool check(Vector3Int position)
   {
      return _placeableobjects.get(position) != null;
   }

   internal void pickup(Vector3Int position)
   {
      placeableobject placedobject = _placeableobjects.get(position);
      if (placedobject == null)
      {
         return;
      }
      itemspawnmanager.instance.spawnitem(targettilemap.CellToWorld(position),
         placedobject.placeditem, 1);
       Destroy(placedobject.targetobject.gameObject);
       _placeableobjects.remove(placedobject);
   }
}