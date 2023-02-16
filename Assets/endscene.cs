using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscene : MonoBehaviour
{
    public string mainGameScene;

    // Update is called once per frame
    void Update()
    {     
  
    }
    public void Again(){
        SceneManager.LoadScene(mainGameScene);
    }
    public void Quit(){
        Application.Quit();
    }
}
