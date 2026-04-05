using UnityEngine;

public interface idamageable 
{
    public void calculatedamage(ref int damage);
    public void applydamage(int damage);

    public void checkstate();
}
