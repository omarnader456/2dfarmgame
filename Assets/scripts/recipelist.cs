using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Data/recipelist")]
public class recipelist : ScriptableObject
{
    public List<craftingrecipe> recipes;
}
