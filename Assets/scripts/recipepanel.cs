using UnityEngine;

public class recipepanel : itempanel
{
   [SerializeField]  recipelist _recipelist;
   [SerializeField]  crafting _crafting;

   public override void show()
   {
      for (int i = 0; i < buttons.Count && i < _recipelist.recipes.Count; i++)
      {
         buttons[i].set(_recipelist.recipes[i].output);
      }
   }

   public override void onclick(int id)
   {
      if (id >= _recipelist.recipes.Count)
      {
         return;
      }
      _crafting.craft(_recipelist.recipes[id]); 
   }
}
