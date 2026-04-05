using UnityEngine;

public class damageable : MonoBehaviour
{
    idamageable _damageable;
    public void takedamage(int damage)
    {
        if (_damageable == null)
        {
            _damageable = GetComponent<idamageable>();
        }

        _damageable.calculatedamage(ref damage);
        _damageable.applydamage(damage);
        Debug.Log(damage + "  damage in takedamage");
        gamemanager.instance.messagesystem.postmessage(transform.position, damage.ToString());
        _damageable.checkstate();

    }
}
