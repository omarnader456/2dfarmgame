using System;
using TMPro;
using UnityEngine;

public enum weatherstates
{
    clear,
    rain,
    heavyrain,
    rainandthunder,
    snow
}

public class weathermanager : timeagent
{
   [Range(0f,1f)] [SerializeField] private float chancetochangeweather = 0.02f;
    
   weatherstates currentstate = weatherstates.clear;
   [SerializeField] private ParticleSystem rainobject;
   [SerializeField] private ParticleSystem heavyobject;
   [SerializeField] private ParticleSystem snowobject;
   [SerializeField] private ParticleSystem rainwithlightning;
   [SerializeField] private TextMeshProUGUI weathertext;

   private void Start()
   {
       init();
       ontimetick += randomweatherchangecheck;
       updateweather();
       updateweathertext();
   }
   public void randomweatherchangecheck(daytimecontroller  _daytimecontroller)
   {
       if (UnityEngine.Random.value < chancetochangeweather)
       {
           randomweatherchange();
       }
   }

   private void randomweatherchange()
   {
       weatherstates newstate =(weatherstates) UnityEngine.Random.Range(0, 
           Enum.GetNames(typeof(weatherstates)).Length);
       changeweather(newstate);
   }

   private void changeweather(weatherstates newstate)
   {
       currentstate = newstate;
       Debug.Log("changeweather " + newstate);
       updateweather();
       updateweathertext();
   }

   public void updateweathertext()
   {
       weathertext.text = "weather: " + currentstate.ToString();
   }

   private void updateweather()
   {
       switch (currentstate)
       {
           case weatherstates.clear:
               rainobject.gameObject.SetActive(false);
               heavyobject.gameObject.SetActive(false);
               rainwithlightning.gameObject.SetActive(false);
               snowobject.gameObject.SetActive(false);
               break;
           case weatherstates.rain:
               rainobject.gameObject.SetActive(true);
               heavyobject.gameObject.SetActive(false);
               rainwithlightning.gameObject.SetActive(false);
               snowobject.gameObject.SetActive(false); 
               break;
           case weatherstates.heavyrain:
               rainobject.gameObject.SetActive(true);
               heavyobject.gameObject.SetActive(true);
               rainwithlightning.gameObject.SetActive(false);
               snowobject.gameObject.SetActive(false);
               break;
           case weatherstates.rainandthunder:
               rainobject.gameObject.SetActive(true);
               heavyobject.gameObject.SetActive(true);
               rainwithlightning.gameObject.SetActive(true);
               snowobject.gameObject.SetActive(false);
               break;
           case weatherstates.snow:
               rainobject.gameObject.SetActive(false);
               heavyobject.gameObject.SetActive(false);
               rainwithlightning.gameObject.SetActive(false);
               snowobject.gameObject.SetActive(true);
               break;
       }
   }
}
