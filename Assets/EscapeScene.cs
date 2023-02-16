using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Esc from user & end if pressed
        if (Input.GetKey("escape")){
            // End Game
            print("escape key was pressed\n");
            Application.Quit();
        }
    }
}
