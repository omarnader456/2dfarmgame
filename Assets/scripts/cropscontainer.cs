using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Data/cropscontainer")]
public class cropscontainer : ScriptableObject
{
    public  List<croptile> crops;

    public croptile get(Vector3Int position)
    {
        return crops.Find(x => x.position == position);
    }

    public void Add(croptile crop)
    {
        crops.Add(crop);
    }
}
