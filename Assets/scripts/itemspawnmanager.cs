using UnityEngine;

public class itemspawnmanager : MonoBehaviour
{
    public static itemspawnmanager instance;

    private void Awake()
    {
        instance = this;
    }

    
    [SerializeField] private GameObject pickupitemprefab;
    public void spawnitem(Vector3 position, item item, int count)
    {
        GameObject obj = Instantiate(pickupitemprefab, position, Quaternion.identity);
        obj.GetComponent<pickupitem>().set(item, count);
    }
}
