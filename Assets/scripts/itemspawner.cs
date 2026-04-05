using UnityEngine;
[RequireComponent(typeof(timeagent))]
public class itemspawner : MonoBehaviour
{
   [SerializeField]  item tospawn;
   [SerializeField]  int count;
   [SerializeField] float spread = 2f;
   [SerializeField]  float probability = 0.5f;


   private void Start()
   {
       timeagent _timeagent = GetComponent<timeagent>();
       _timeagent.ontimetick += spawn;
   }
   void spawn(daytimecontroller _daytimecontroller)
   {
       if (UnityEngine.Random.value < probability)
       {
           Vector3 position = transform.position;
           position.x += spread * UnityEngine.Random.value - spread / 2;
           position.y += spread * UnityEngine.Random.value - spread / 2;
           itemspawnmanager.instance.spawnitem(position, tospawn, count); 
       }  
       
   }
}
