using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class trading : MonoBehaviour
{
   private store _store;
   [SerializeField] private GameObject storepanel;
   [SerializeField] private GameObject inventorypanel;
   [SerializeField] private GameObject storeParentPanel;
   private currency money;
   itemstorepanel _itemstorepanel;
   [SerializeField] itemcontainer playerinventory;
   [SerializeField] itempanel inventoryitempanel;

   private void Awake()
   {
     money = GetComponent<currency>(); 
     _itemstorepanel = storepanel.GetComponent<itemstorepanel>();
   }

   public void begintrading(store _store)
   {
      this._store = _store;
      Debug.Log("begintrading");
      _itemstorepanel.setinventory(_store.storecontent);
      storeParentPanel.SetActive(true);
      storepanel.SetActive(true);
      inventorypanel.SetActive(true);
   }

   public void stoptrading()
   {
      Debug.Log("stop trading");
      _store  = null;
      storepanel.SetActive(false);
      storeParentPanel.SetActive(false);
      inventorypanel.SetActive(false);
   }
   public void sellitem()
   {
      if (gamemanager.instance.draganddropcontroller.checkforsale() == true)
      {
         Debug.Log("sellitem");
         itemslot itemtosell = gamemanager.instance.draganddropcontroller.itemslot;
         int moneygained = itemtosell.itm.stackable == true
            ?(int) (itemtosell.itm.price * itemtosell.count * _store.buyfromplayermultiplier)
            : (int) (itemtosell.itm.price * _store.buyfromplayermultiplier);
         money.add(moneygained);
         itemtosell.clear();
         gamemanager.instance.draganddropcontroller.updateicon();
      }
   }

   public void buyitem(int id)
   {
      item itemtobuy = _store.storecontent.slots[id].itm;
      int totalprice =(int) (itemtobuy.price * _store.selltoplayermultiplier);
      if (money.check(totalprice) == true)
      {
         money.decrease(totalprice);
         playerinventory.add(itemtobuy);
         inventoryitempanel.show();
      }
   }
}
