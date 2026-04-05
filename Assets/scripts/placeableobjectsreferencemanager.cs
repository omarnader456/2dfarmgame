using UnityEngine;

public class placeableobjectsreferencemanager : MonoBehaviour
{
   public placeableobjectmanager _placeableobjectmanager;

   public void place(item  _item, Vector3Int position)
   {
      if (_placeableobjectmanager == null)
      {
         Debug.Log("placeableobjectmanager is null");
         return;
      }
      _placeableobjectmanager.place(_item, position);
   }

   public bool check(Vector3Int position)
   {
      if (_placeableobjectmanager == null)
      {
         Debug.Log("placeableobjectmanager is null");
         return false;
      }
      return _placeableobjectmanager.check(position);
   }

   internal void pickup(Vector3Int position)
   {
      if (_placeableobjectmanager == null)
      {
         Debug.Log("placeableobjectmanager is null");
         return;
      } 
      _placeableobjectmanager.pickup(position);
   }

}
