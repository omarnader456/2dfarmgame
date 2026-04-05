using System.Collections.Generic;
using UnityEngine;

public class panelgroup : MonoBehaviour
{
   public List<GameObject> panels;

   public void show(int idpanel)
   {
       for (int i = 0; i < panels.Count; i++)
       {
           panels[i].SetActive(i == idpanel);
       }
}
}
