using System;
using UnityEngine;

public class spawnedobject : MonoBehaviour
{
    [Serializable]
    public class savespawnedobjectdata
    {
        public int objectid;
        public Vector3 worldposition;

        public savespawnedobjectdata(int objectid, Vector3 worldposition)
        {
            this.objectid = objectid;
            this.worldposition = worldposition;
        }
    }

    public int objectid;
    
    public void spawnedobjectdestroy()
    {
        transform.parent.GetComponent<objectspawner>().spawnedobjectdestroyed(this);
    }
}
