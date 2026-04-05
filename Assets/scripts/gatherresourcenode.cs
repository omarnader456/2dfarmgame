using System.Collections.Generic;
using UnityEngine;


public enum resourcenodetype
{
   undefined,
   tree,
   ore
}
[CreateAssetMenu(menuName = "Data/toolaction/gatherresourcenode")]
public class gatherresourcenode : toolaction
{
   [SerializeField] float interactablearea = 1f;
   [SerializeField] private List<resourcenodetype> canhitnodesoftype;
   public override bool onapply(Vector2 worldpoint)
   {
      Collider2D[] colliders = Physics2D.OverlapCircleAll(worldpoint, interactablearea);
      foreach (Collider2D c in colliders )
      {
         toolhit hit = c.GetComponent<toolhit>();
         if (hit is not null)
         {
            if (hit.canbehit(canhitnodesoftype) == true)
            {
               hit.hit();
               return true; 
            }
         }
      }
      return false; 
   }
}
