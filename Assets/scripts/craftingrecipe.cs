using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu( menuName = "Data/recipe")]
public class craftingrecipe : ScriptableObject
{
    public List<itemslot> elements;
    public itemslot output;
}
