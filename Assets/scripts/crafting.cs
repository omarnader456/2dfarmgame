using UnityEngine;

public class crafting : MonoBehaviour
{
  [SerializeField] private itemcontainer inventory;
  public void craft(craftingrecipe recipe)
  {
    if (inventory.checkfreespace() == false)
    {
      Debug.Log("not enough space to fit the item after crafting");
      return;
    }
    
    for (int i = 0; i < recipe.elements.Count; i++)
    {
      if (inventory.checkitem(recipe.elements[i]) == false)
      {
        Debug.Log("crafting recipe elements are not present in the inventory");
        return;
      }
    }

    

    for (int i = 0; i < recipe.elements.Count; i++)
    {
      inventory.remove(recipe.elements[i].itm, recipe.elements[i].count);
    }
    
    inventory.add(recipe.output.itm, recipe.output.count);
    
    inventorypanel panel = FindFirstObjectByType<inventorypanel>();
  } 
}
