using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/crop")]
public class crop : ScriptableObject
{
   public int timetogrow = 10;
   public item yield;
   public int count = 1;
   public List<Sprite> sprite;
   public List<int> growthstagetime;
}
