using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class daytimecontroller : MonoBehaviour
{
    private float time;
    const float secondsinday = 86400;
    const float phaselength = 900f;
    private const float phaseinday = 96f;
    [SerializeField] private Color nightcolor;
    [SerializeField] private Color daycolor = Color.white;
    [SerializeField] private AnimationCurve nightcurve;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timescale = 60f;
    [SerializeField] private Light2D globallight;
    public int days;
    List<timeagent> agents;
    [SerializeField]  float startattime = 28800f;
    [SerializeField] private float morningtime = 28800f;
    public float _currenthour { get { return time / 3600f; } }
    private void Awake()
    {
       agents = new List<timeagent>(); 
    }

    private void Start()
    {
        time = startattime;
    }

    public void subscribe(timeagent _timeagent)
    {
        agents.Add(_timeagent);
    }

    public void unsubscribe(timeagent _timeagent)
    {
        agents.Remove(_timeagent);
    }

    float hours
    {
        get { return time / 3600f; }
    }

    public void timecalculation()
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        text.text = string.Format("time:{0:00}:{1:00}" ,t.Hours, t.Minutes);
    }

    public void daylight()
    {
        float v = nightcurve.Evaluate(hours);
        Color c = Color.Lerp(daycolor, nightcolor, v);
        globallight.color = c;

    }

    private int oldphase = -1;
    public void timeagents()
    {
        if (oldphase == -1)
        {
            oldphase = calculatephase();
        }
        int currentphase = calculatephase();
        while (oldphase < currentphase)
        {
            oldphase += 1;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke(this);
            } 
        }
    }

    private int calculatephase()
    {
        return (int)(time / phaselength) + (int)(days*phaseinday);
    }

    private void Update()
    {
        time += Time.deltaTime * timescale;
       timecalculation();
       daylight();
        if (time > secondsinday)
        {
            nextday();
        }

       timeagents();
       if (Input.GetKeyDown(KeyCode.T))
       {
           skiptime(hours: 4);
       }
    }

    private void nextday()
    {
        time -= secondsinday;
        days += 1;
        
    }

    public void skiptime(float seconds = 0, float minutes =0, float hours = 0)
    {
        float timetoskip = seconds;
        timetoskip += minutes * 60f;
        timetoskip += hours * 3600f;
        
        time += timetoskip;
    }

    public void skiptomorning()
    {
        float secondstoskip = 0f;
        if (time > morningtime)
        {
            secondstoskip += secondstoskip - time + morningtime;
        }
        else
        {
            secondstoskip += morningtime - time;
        }
        skiptime(secondstoskip);
    }
}
