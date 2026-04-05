using UnityEngine;

public class itemcontainerpanel :itempanel 
{
    public override void onclick(int id)
    {
        gamemanager.instance.draganddropcontroller.onclick(inventory.slots[id]);
        show();
    } 
}
