using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Data/item")]
public class item : ScriptableObject
{
    public string name;
    public bool stackable;
    public Sprite icon;
    public toolaction onaction;
    public toolaction ontilemapaction;
    public toolaction _onitemused;
    public crop _crop;
    public bool iconhighlight;
    public GameObject itemprefab;
    public int id;
    public bool isweapon;
    public int damage = 10;
    public int price = 100;
    public bool canbesold = true;
}
