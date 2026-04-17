using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

[RequireComponent(typeof(timeagent))]
public class objectspawner : MonoBehaviour
{
   [SerializeField] private float spawnareaheight = 1f;
   [SerializeField] float spawnareawidth = 1f;
   [SerializeField] private GameObject[] _spawn;
   public int length;
   [SerializeField] float probability = 0.1f;
   [SerializeField] private int spawncount = 1;
   [SerializeField] private bool onetime = false;
   public List<spawnedobject> spawnobjects;
   [SerializeField] private jsonstringlist targetsavejsonlist;
   [SerializeField] private int idinlist = -1;
   [SerializeField] private int objectspawnlimit = -1;
   private void Start()
   {
      length = _spawn.Length;
      if (onetime == false)
      {
         timeagent _timeagent = GetComponent<timeagent>();
         _timeagent.ontimetick += spawn; 
         spawnobjects = new List<spawnedobject>();
         loaddata();
      }
      else
      {
         spawn(null);
      } 
   }

   private void loaddata()
   {
      if (checkjson() == false)
      {
         return;
      }
      load(targetsavejsonlist.getstring(idinlist));
   }

   public void spawn(daytimecontroller _daytimecontroller)
   {
      Debug.Log("spawn");
      if (Random.value > probability)
      {
         return;
      }
      Debug.Log("after spawn method");
      if (spawnobjects != null)
      {
         if (objectspawnlimit <= spawnobjects.Count && objectspawnlimit != -1)
         {
            Debug.Log("return spawn limit reached " + objectspawnlimit);
            return;
         }
      }

      for (int i = 0; i < spawncount; i++)
      {
         Debug.Log("spawn for 1");
         int id = Random.Range(0, _spawn.Length);
         GameObject obj = Instantiate(_spawn[id]);
         Transform t = obj.transform;
         Debug.Log(obj+" spawn method objectid "+id);
         t.SetParent(transform);
         if (onetime == false)
         {
            spawnedobject _spawnobject = obj.AddComponent<spawnedobject>();
            spawnobjects.Add(_spawnobject);
            _spawnobject.objectid = id;
            Debug.Log(_spawnobject.objectid + " " + _spawnobject +" spawn method");
         }
         Vector3 position = transform.position;
         position.x += UnityEngine.Random.Range(-spawnareawidth, spawnareawidth);
         position.y += UnityEngine.Random.Range(-spawnareaheight, spawnareaheight);
         t.position = position; 
         Debug.Log(t+" spawn method ");
      } 
   }

   public class tosave
   {
      public List<spawnedobject.savespawnedobjectdata> spawnobjectdata;

      public tosave()
      {
         spawnobjectdata = new List<spawnedobject.savespawnedobjectdata>();
      }
   }
   public string read()
   {
      tosave tosave = new tosave();
      for (int i = 0; i < spawnobjects.Count; i++)
      {
         tosave.spawnobjectdata.Add(new spawnedobject.savespawnedobjectdata(spawnobjects[i].objectid,
            spawnobjects[i].transform.position));
      }

      return JsonUtility.ToJson(tosave);
   }

   public void load(string json)
   {
      if (json == "" || json == "{}" ||json == null )
      {
         return;
      }
      tosave toload = JsonUtility.FromJson<tosave>(json);
      for (int i = 0; i < toload.spawnobjectdata.Count; i++)
      {
         spawnedobject.savespawnedobjectdata data = toload.spawnobjectdata[i];
         GameObject obj = Instantiate(_spawn[data.objectid]);
         obj.transform.position = data.worldposition;
         obj.transform.SetParent(transform);
         spawnedobject _spawnobject = obj.AddComponent<spawnedobject>();
         _spawnobject.objectid = data.objectid;
         spawnobjects.Add(_spawnobject);
      }
   }

   public bool checkjson()
   {
      if (onetime == true)
      {
         return false;
      }

      if (targetsavejsonlist == null)
      {
         Debug.LogError("targetsavejsonlist is null (used to save data on spawnable objects");
         return false;
      }

      if (idinlist == -1)
      {
         Debug.LogError("idinlist not assigned can't perform save");
         return false;
      }
      return true;
   }

   public void savedata()
   {
      if (checkjson() == false)
      {
         return;
      }

      string jsonstring = read();
      targetsavejsonlist.setstring(jsonstring, idinlist);
   }
   
   private void OnDestroy()
   {
      if (onetime == true)
      {
         return;
      }
     savedata(); 
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
     Gizmos.DrawWireCube(transform.position, new Vector3(spawnareawidth*2, spawnareaheight*2)); 
   }

   public void spawnedobjectdestroyed(spawnedobject spawnedobject)
   {
      spawnobjects.Remove(spawnedobject);
   }
}
