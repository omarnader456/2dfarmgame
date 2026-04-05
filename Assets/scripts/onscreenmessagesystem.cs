using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class onscreenmessage
{
  public GameObject obj;
  public float timetolive;

  public onscreenmessage(GameObject obj)
  {
    this.obj = obj;
  }
}
public class onscreenmessagesystem : MonoBehaviour
{
  [SerializeField] private GameObject textprefab;
  private List<onscreenmessage> _onscreenmessagelist;
  private List<onscreenmessage> openlist;
  [SerializeField] private float horizontalscatter = 0.5f;
  [SerializeField] float verticalscatter = 0.5f;
  [SerializeField] private float timetolive = 5f;

  private void Awake()
  {
   _onscreenmessagelist = new List<onscreenmessage>(); 
   openlist = new List<onscreenmessage>();
  }

  private void Update()
  {
    for (int i = _onscreenmessagelist.Count - 1; i >= 0; i--)
    {
      _onscreenmessagelist[i].timetolive-=Time.deltaTime;
      if (_onscreenmessagelist[i].timetolive <= 0)
      {
        _onscreenmessagelist[i].obj.SetActive(false);
        openlist.Add(_onscreenmessagelist[i]);
        _onscreenmessagelist.RemoveAt(i);
      } 
    }
    
   
  }

  public void postmessage(Vector3 worldposition, string message)
  {
    worldposition.z = -3f;
    worldposition.x -= 1;
    worldposition.y -= 1;
    worldposition.x += Random.Range(-horizontalscatter, horizontalscatter);
    worldposition.y += Random.Range(-verticalscatter, verticalscatter);
    
    if (openlist.Count > 0)
    {
      reuseobjectfromopenlist(worldposition, message);
    }
    else
    {
      createnewonscreenmessageobject(worldposition, message);
    }
  }

  public void reuseobjectfromopenlist(Vector3 worldposition, string message)
  {
    onscreenmessage oscrnmsg = openlist[0];
    oscrnmsg.obj.SetActive(true);
    oscrnmsg.timetolive = timetolive;
    oscrnmsg.obj.GetComponent<TextMeshPro>().text = message;
    oscrnmsg.obj.transform.position = worldposition;
    openlist.RemoveAt(0);
    _onscreenmessagelist.Add(oscrnmsg); 
  }

  public void createnewonscreenmessageobject(Vector3 worldposition, string message)
  {
    Debug.Log(worldposition + "  postmessage");
    GameObject textgo = Instantiate(textprefab, transform);
    textgo.transform.position = worldposition;
    Debug.Log("postmessage "+ textgo.name);
    
    TextMeshPro tmp = textgo.GetComponent<TextMeshPro>();
    Debug.Log( "postmessage tmp component "+ tmp);
    tmp.text = message;
    Debug.Log(message + "  postmessage");
    onscreenmessage _onscreenmessage = new onscreenmessage(textgo);
    _onscreenmessage.timetolive = timetolive;
    _onscreenmessagelist.Add(_onscreenmessage); 
  }
}
