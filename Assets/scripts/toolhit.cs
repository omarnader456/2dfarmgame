using System.Collections.Generic;
using UnityEngine;

public class toolhit: MonoBehaviour
{
    public virtual void hit()
    {
        
    }

    public virtual bool canbehit(List<resourcenodetype> _canbehit)
    {
        return true;
    }
}
