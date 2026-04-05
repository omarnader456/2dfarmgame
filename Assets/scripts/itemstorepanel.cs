using UnityEngine;

public class itemstorepanel : itempanel
{
    [SerializeField] private trading _trading;

    public override void onclick(int id)
    {
        if (gamemanager.instance.draganddropcontroller.itemslot.itm == null)
        {
            buyitem(id);
        }
        else
        {
            Debug.Log("onclick then sell item");
            sellitem();
        }
        show();
    }

    private void buyitem(int id)
    {
        _trading.buyitem(id);
    }

    private void sellitem()
    {
        Debug.Log("sellitem");
        _trading.sellitem();
    }

    
}

   
