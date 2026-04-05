using UnityEngine;

public class store : interactable
{ 
   public itemcontainer storecontent;
   public float buyfromplayermultiplier = 0.5f;
   public float selltoplayermultiplier = 1.5f;
   public override void interact(character _character)
   {
      trading _trading = _character.GetComponent<trading>();
      if (_trading == null)
      {
         return;
      }
      _trading.begintrading(this);
   }
}
