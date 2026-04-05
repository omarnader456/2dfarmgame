using UnityEngine;

public class playerrespawn : MonoBehaviour
{

    [SerializeField] private Vector3 respawnpointposition;

    [SerializeField] private string respawnpointscene;

    public void startrespawn()
    {
        Debug.Log("startrespawn playerrespawn");
        gamescenemanager.instance.respawn(respawnpointposition, respawnpointscene);
    }
}
