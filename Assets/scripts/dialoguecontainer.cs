using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/dialogue/dialoguecontainer")]
public class dialoguecontainer : ScriptableObject
{
    public List<string> lines;
    public actor _actor;
}

