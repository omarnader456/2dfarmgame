using System;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class placeableobject
{
    public item placeditem;
    public Transform targetobject;
    public Vector3Int positionongrid;
    public string objectstate;

    public placeableobject(item _item,  Vector3Int position)
    {
        placeditem = _item;
        positionongrid = position;
    }
}

[CreateAssetMenu( menuName = "Data/placeableobjectcontainer")]
public class placeableobjectcontainer : ScriptableObject
{
    public List<placeableobject> placeableobjects;

    internal placeableobject get(Vector3Int position)
    {
        return placeableobjects.Find(x => x.positionongrid == position);
    }

    internal void remove(placeableobject placedobject)
    {
        placeableobjects.Remove(placedobject);
    }
}