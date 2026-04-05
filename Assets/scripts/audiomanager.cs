using System;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;
   [SerializeField] private GameObject audiosourceprefab;
   [SerializeField] private int audiosourcecount;
   private List<AudioSource> audiosources;

   private void Awake()
   {
       instance = this;
   }
   private void Start()
   {
       init();
   }

   private void init()
   {
       audiosources = new List<AudioSource>();
       for (int i = 0; i < audiosourcecount ; i++)
       {
           GameObject obj = Instantiate(audiosourceprefab, transform);
           obj.transform.localPosition = Vector3.zero;
           audiosources.Add(obj.GetComponent<AudioSource>());
       }
   }

   public void play(AudioClip audioclip)
   {
       if (audioclip == null)
       {
           return;
       }
       AudioSource audiosource = getfreeaudiosource();
       audiosource.clip = audioclip;
       audiosource.Play();
   }

   private AudioSource getfreeaudiosource()
   {
       for (int i = 0; i < audiosources.Count; i++)
       {
           if (audiosources[i].isPlaying == false)
           {
               return audiosources[i];
           }
       }

       return audiosources[0];
   }
}
