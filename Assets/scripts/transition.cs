using Unity.Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum transitiontype
{
    warp,
    scene
}
public class transition : MonoBehaviour
{
    [SerializeField]  transitiontype _transitiontype;
     [SerializeField] Transform destination;
     [SerializeField] Vector3 targetposition;
     [SerializeField]  string scenenametotransition;
     [SerializeField] private Collider2D confiner;

     private cameraconfiner _cameraconfiner;
    void Start()
    {
        if (confiner != null)
        {
            _cameraconfiner = FindObjectOfType<cameraconfiner>();
        }
    }

    internal void initiatetransition(Transform totransition)
    {
        switch (_transitiontype)
        {
            case transitiontype.warp:
                Debug.Log("before camerconfiner in warp");
                bool test = _cameraconfiner != null;
                Debug.Log("cameraconfiner in warp is "+ test);
                if (_cameraconfiner != null)
                {
                    Debug.Log("initiating warp cameraconfiner is " + test );
                   _cameraconfiner.updatebounds(confiner);
                }
                
                CinemachineCore.OnTargetObjectWarped(totransition, 
                    destination.position - totransition.position);
                totransition.position = new Vector3(destination.position.x, 
                    destination.position.y, destination.position.z);
                break;
            case transitiontype.scene:
                gamescenemanager.instance.initswitchscene(scenenametotransition, targetposition);
                break;
        }
    }
    private void OnDrawGizmos()
    {
        if (_transitiontype == transitiontype.scene)
        {
            Handles.Label(transform.position, "to " + scenenametotransition);
        }

        if (_transitiontype == transitiontype.warp)
        {
            Gizmos.DrawLine(transform.position,destination.position);
        }
    }
}
