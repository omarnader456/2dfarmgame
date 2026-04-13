using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamescenemanager : MonoBehaviour
{
  public static gamescenemanager instance;
  [SerializeField]  screentint _screentint;
  [SerializeField]  cameraconfiner _cameraconfiner;
   AsyncOperation unload;
   AsyncOperation load;
   private bool respawntransition;
  private void Awake()
  {
      instance = this;
  }

  private string currentscene;

  private void Start()
  {
     currentscene = SceneManager.GetActiveScene().name; 
  }

  public void initswitchscene(string to, Vector3 targetposition)
  {
      StartCoroutine(_transition(to,targetposition));
  }
  IEnumerator _transition(string to, Vector3 targetposition)
  {
     
      _screentint.tint();
      Debug.Log(to + " gamescenemanager _transition coroutine function");
      Debug.Log(targetposition + " gamescenemanager _transition coroutine function");
      yield return new WaitForSeconds(1f / (_screentint.speed + 0.1f));
      dataorchestrator orchestrator = FindFirstObjectByType<dataorchestrator>();
      if (dataorchestrator.instance != null) {
          dataorchestrator.instance.savegame();
      }
      load = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
      yield return new WaitUntil(() => load.isDone);
      

      SceneManager.SetActiveScene(SceneManager.GetSceneByName(to));
      Debug.Log(to + " gamescenemanager _transition coroutine function");


      unload = SceneManager.UnloadSceneAsync(currentscene);
      yield return new WaitUntil(() => unload.isDone);

      currentscene = to;

      Transform playertransform = gamemanager.instance.player.transform;
      CinemachineCore.OnTargetObjectWarped(playertransform, 
          targetposition - playertransform.position);
      playertransform.position = new Vector3(targetposition.x, targetposition.y, playertransform.position.z);
      if (respawntransition)
      {
          playertransform.GetComponent<character>().fullheal();
          playertransform.GetComponent<disablecontrols>().enablecontrol();
          respawntransition = false;
      }
      Debug.Log(playertransform.position + " gamescenemanager _transition coroutine function");

      var _readController = FindFirstObjectByType<tilemapreadcontroller>();
      if (_readController != null) _readController.RefreshTilemap();
    
      _cameraconfiner.updatebounds();
      _screentint.untint();
      Debug.Log(to + " gamescenemanager _transition coroutine function");
      Debug.Log(playertransform.position + " gamescenemanager _transition coroutine function");

  }

  public void movecharacter(Vector3 targetposition)
  {
      Transform playertransform = gamemanager.instance.player.transform;
      CinemachineCore.OnTargetObjectWarped(playertransform, 
          targetposition - playertransform.position);
      playertransform.position = new Vector3(targetposition.x, targetposition.y, playertransform.position.z);
      if (respawntransition)
      {
          playertransform.GetComponent<character>().fullheal();
          playertransform.GetComponent<character>().fullrest();
          playertransform.GetComponent<disablecontrols>().enablecontrol();
          respawntransition = false;
      }
      Debug.Log(playertransform.position + " gamescenemanager _transition coroutine function");

  }
  
  public void respawn(Vector3 respawnpointposition, string respawnpointscene)
  {
      Debug.Log("respawn in gamescenemanager");
      Debug.Log("respawn in gamescenemanager: cureentscene "+currentscene+" respawnpointscene "+respawnpointscene);
      respawntransition = true;
      Debug.Log("respawn in gamescenemanager"+ currentscene);
      if (currentscene != respawnpointscene)
      {
          initswitchscene(respawnpointscene, respawnpointposition);
      }
      else
      {
          movecharacter(respawnpointposition);
      }
  }
}
