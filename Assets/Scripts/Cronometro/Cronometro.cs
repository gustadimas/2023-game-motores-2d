using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    private float StartTime;

    void Start()
    {
        StartTime = Time.time;
    }

    void Update()
    {
        float TimerControl = Time.time - StartTime;
        string mins = ((int)TimerControl / 60).ToString("00");
        string segs = (60 - TimerControl % 60).ToString("00");
        string milisegs = ((TimerControl * 100) % 100).ToString("00");

        string TimerString = string.Format("{00}:{01}:{02}", mins, segs, milisegs);

        GetComponent<Text>().text = TimerString.ToString();

        if (segs.Equals("00"))
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
