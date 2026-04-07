using UnityEngine;

[CreateAssetMenu( menuName = "Data/playerdata")]
public class playerdata : ScriptableObject
{
    public string playername;
    public gender playergender;
    public int saveslot;
}
