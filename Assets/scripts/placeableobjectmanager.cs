using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public class savedplacedobject {
    public string itemname;
    public Vector3Int position;
    public string state;
}

[Serializable]
public class placedobjectssavedata {
    public List<savedplacedobject> placedobjects = new List<savedplacedobject>();
}

public class placeableobjectmanager : MonoBehaviour, ipersistant 
{
   [SerializeField] private placeableobjectcontainer _placeableobjects;
   [SerializeField] private Tilemap targettilemap;

   private void Start()
   {
       gamemanager.instance.GetComponent<placeableobjectsreferencemanager>()._placeableobjectmanager = this;
      
       visualizemap();
   }

   public void place(item _item, Vector3Int positionongrid)
   {
      if (check(positionongrid) == true) return;
      
      placeableobject _placeableobject = new placeableobject(_item,positionongrid);
      visualizeitem(_placeableobject);
      _placeableobjects.placeableobjects.Add(_placeableobject);
   }

   private void visualizemap()
   {
       for (int i = 0; i < _placeableobjects.placeableobjects.Count; i++)
       {
           if (_placeableobjects.placeableobjects[i].targetobject == null)
           {
               visualizeitem(_placeableobjects.placeableobjects[i]);
           }
       }
   }

   private void visualizeitem(placeableobject _placeableobject)
   {
      GameObject obj = Instantiate(_placeableobject.placeditem.itemprefab);
      obj.transform.parent = transform;
      Vector3 position = targettilemap.CellToWorld(_placeableobject.positionongrid) + targettilemap.cellSize / 2;
      obj.transform.position = position;
      
      ipersistant persistant = obj.GetComponent<ipersistant>();
      if (persistant != null)
      {
         persistant.load(_placeableobject.objectstate);
      }
      _placeableobject.targetobject = obj.transform;
   }

   public bool check(Vector3Int position)
   {
      return _placeableobjects.get(position) != null;
   }

   internal void pickup(Vector3Int position)
   {
      placeableobject placedobject = _placeableobjects.get(position);
      if (placedobject == null) return;
      
      itemspawnmanager.instance.spawnitem(targettilemap.CellToWorld(position), placedobject.placeditem, 1);
      Destroy(placedobject.targetobject.gameObject);
      _placeableobjects.remove(placedobject);
   }


   public string read()
   {
        placedobjectssavedata data = new placedobjectssavedata();
        foreach (var p in _placeableobjects.placeableobjects)
        {
            savedplacedobject _savedplacedobject = new savedplacedobject();
            _savedplacedobject.itemname = p.placeditem.name;
            _savedplacedobject.position = p.positionongrid;
            
            if (p.targetobject != null)
            {
                ipersistant persistant = p.targetobject.GetComponent<ipersistant>();
                if (persistant != null)
                {
                    p.objectstate = persistant.read();
                }
            }
            _savedplacedobject.state = p.objectstate;
            data.placedobjects.Add(_savedplacedobject);
        }
        return JsonUtility.ToJson(data);
   }

   public void load(string jsonstring)
   {
        for (int i = 0; i < _placeableobjects.placeableobjects.Count; i++)
        {
            if (_placeableobjects.placeableobjects[i] != null && _placeableobjects.placeableobjects[i].targetobject != null)
            {
                Destroy(_placeableobjects.placeableobjects[i].targetobject.gameObject);
            }
        }
        _placeableobjects.placeableobjects.Clear();

        if (string.IsNullOrEmpty(jsonstring) || jsonstring == "{}") return;

        placedobjectssavedata data = JsonUtility.FromJson<placedobjectssavedata>(jsonstring);
        foreach (var _savedplacedobject in data.placedobjects)
        {
            item foundItem = gamemanager.instance.itemdb.items.Find(x => x.name == _savedplacedobject.itemname);
            if (foundItem != null)
            {
                placeableobject po = new placeableobject(foundItem, _savedplacedobject.position);
                po.objectstate = _savedplacedobject.state;
                _placeableobjects.placeableobjects.Add(po);
            }
        }
        
        visualizemap();
   }
}