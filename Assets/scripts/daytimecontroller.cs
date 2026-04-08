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
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float timescale = 60f;
    [SerializeField] private Light2D globallight;
    public int days;
    List<timeagent> agents;
    [SerializeField]  float startattime = 28800f;
    [SerializeField] private float morningtime = 28800f;
    public float _currenthour { get { return time / 3600f; } }
    public enum daysoftheweek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public enum seasons
    {
        Spring,
        Summer, 
        Winter,
        Autumn,
    }

    private seasons _seasons;
    private const int seasonlength = 7;
    public daysoftheweek _daysoftheweek;
    [SerializeField] private TextMeshProUGUI seasonstext;
    public int totaldays;
    private void Awake()
    {
       agents = new List<timeagent>(); 
    }

    private void Start()
    {
        time = startattime;
        updateweek();
        updateseasontext();
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
        return (int)(time / phaselength) + (int)(totaldays*phaseinday);    }

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
        totaldays += 1;
        int daynumber = (int) _daysoftheweek;
        daynumber += 1;
        if (daynumber >= 7)
        {
            daynumber = 0;
        }
        _daysoftheweek = (daysoftheweek) daynumber;
        _text.text = "week day: "+_daysoftheweek.ToString();
        updateweek();
        if (days >= seasonlength)
        {
            nextseason();
        }
    }

    private void nextseason()
    {
        days = 0;
        int seasonnumber = (int)_seasons;
        seasonnumber += 1;
        if (seasonnumber >= 4)
        {
            seasonnumber = 0;
        }
        _seasons = (seasons) seasonnumber;
        updateseasontext();
    }

    public void updateseasontext()
    {
        seasonstext.text = "season " + _seasons.ToString();
    }

    public void updateweek()
    {
        _text.text = "week day: "+_daysoftheweek.ToString();
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
