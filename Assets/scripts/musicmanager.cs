using System;
using System.Collections;
using UnityEngine;

public class musicmanager : MonoBehaviour
{
   [SerializeField] private AudioSource audiosource;
   [SerializeField] private AudioClip playonstart;
   [SerializeField]  float timetoswitch;
   public static musicmanager instance;

   private void Awake()
   {
      instance = this;
   }
   private void Start()
   {
      play(playonstart, true);
   }

   

   public void play(AudioClip musictoplay, bool interrupt = false)
   {
      if (musictoplay == null)
      {
         return;
      }
      if (interrupt == true)
      {
         audiosource.volume = 1f;
         audiosource.clip = musictoplay;
         audiosource.Play(); 
      }
      else
      {
        switchto =  musictoplay;
        StartCoroutine(smoothswitchmusic());
      } 
   }

    AudioClip switchto;
   IEnumerator smoothswitchmusic()
   {
      float volume = 1f;
      while (volume > 0f)
      {
         volume -= Time.deltaTime/timetoswitch;
         if (volume < 0f)
         {
            volume = 0f;
         }
         audiosource.volume = volume;
         yield return new WaitForEndOfFrame();
      }
      play(switchto,true);
   }
}
