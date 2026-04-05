using System;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;

public class inventorypanel : itempanel
{
    public override void onclick(int id)
    {
        gamemanager.instance.draganddropcontroller.onclick(inventory.slots[id]);
        show();
    }
    
}
