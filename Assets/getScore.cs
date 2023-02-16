using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getScore : MonoBehaviour
{
    public Text playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore.text  =  PlayerPrefs.GetString("score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
