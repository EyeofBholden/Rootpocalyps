using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stopwatch : MonoBehaviour
{
    private float timer;
    private float seconds;
    private float minutes;
    private float hours;
    public string output;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        calculateTime();
    }
    void calculateTime(){
        timer += Time.deltaTime;
        seconds = timer % 60;
        minutes = timer / 60;
        hours = timer / 3600;
        output = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    void OnDisable()
    {
        PlayerPrefs.SetString("score", output);
    }
}
