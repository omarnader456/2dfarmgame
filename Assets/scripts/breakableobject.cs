using UnityEngine;

public class breakableobject : MonoBehaviour, idamageable
{
    [SerializeField] int hp = 10;
    public void calculatedamage(ref int damage)
    {
        damage =1;
    }

    public void applydamage(int damage)
    {
        hp-=damage;
        Debug.Log(damage + "  damage in takedamage");
    }

    public void checkstate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
