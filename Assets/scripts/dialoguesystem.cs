using System;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevelPhysics2D;
using UnityEngine.UI;

public class dialoguesystem : MonoBehaviour
{
   [SerializeField]  TextMeshProUGUI targettext;
   [SerializeField]  TextMeshProUGUI nametext;
   [SerializeField]  Image portrait;
   int currenttextline;

   public dialoguecontainer currentdialogue;
   [Range(0f,1f)]
   [SerializeField]  float visibletextpercent;

   [SerializeField]  float timeperletter = 0.05f;
    float totaltimetotype, currenttime;
    string linetoshow;
   

   private void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           pushtext();
       }
       typeouttext();
   }

   private void typeouttext()
   {
       if (visibletextpercent >= 1f)
       {
           return;
           
       }
       currenttime+=Time.deltaTime;
       visibletextpercent = currenttime / totaltimetotype;
       visibletextpercent = Mathf.Clamp(visibletextpercent, 0, 1f);
       updatetext();
       }

   void updatetext()
   {
       int lettercount = (int)( linetoshow.Length * visibletextpercent);
       targettext.text = linetoshow.Substring(0, lettercount);
   }
   private void pushtext()
   {
       if (visibletextpercent < 1f)
       {
           visibletextpercent = 1f;
           updatetext();
           return;
       }
       if (currenttextline >= currentdialogue.lines.Count)
       {
           conclude();
       }
       else
       {
           cycleline();
       }
   }

   void cycleline()
   {
       linetoshow = currentdialogue.lines[currenttextline];
       totaltimetotype = linetoshow.Length * timeperletter;
       currenttime = 0f;
       visibletextpercent = 0f;
       targettext.text = "";
       currenttextline += 1;
   }

   public void initialize(dialoguecontainer _dialoguecontainer)
   {
       show(true);
       currentdialogue = _dialoguecontainer;
       currenttextline = 0;
      cycleline();
      updateportrait();
   }

   private void updateportrait()
   {
       portrait.sprite = currentdialogue._actor.portrait;
       nametext.text = currentdialogue._actor.name;
   }

   private void show(bool b)
   {
       gameObject.SetActive(b);
   }

   private void conclude()
   {
       Debug.Log("the dialogue has ended");
       show(false);
   }
}
