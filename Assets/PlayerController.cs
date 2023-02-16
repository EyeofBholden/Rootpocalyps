using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.5f;
    private bool cantGoLeft = false;
    private bool cantGoRight = false;


     

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
         // Flip character based on which key is pressed
        if(horizontalInput > 0){ // Character travelling right
            gameObject.transform.localScale = new Vector3(-1,1,1);
            cantGoLeft = false;
         } else if(horizontalInput < 0) {
            gameObject.transform.localScale = new Vector3(1,1,1);
            cantGoRight = false;
        }
        if (horizontalInput > 0 && cantGoRight){
            horizontalInput = 0;
        }
        if (horizontalInput < 0 && cantGoLeft){
            horizontalInput = 0;
        }
        Vector3 direction =  new Vector3(horizontalInput, 0f, 0f);
        
        transform.Translate(direction * speed * Time.deltaTime);

        // Check for Esc from user & end if pressed
        if (Input.GetKey("escape")){
            // End Game
            print("escape key was pressed\n");
            Application.Quit();
        }
        
    }
    void OnTriggerEnter2D(Collider2D col){
        print(col.gameObject.name);
        print(col.gameObject.tag);
        float horizontalInput = Input.GetAxis("Horizontal");
        if (col.gameObject.name == "Right Wall")
        {
            cantGoRight = true;    
        }
        if (col.gameObject.name == "Left Wall")
        {
            cantGoLeft = true;
        }
        if (col.tag == "root"){
            if (horizontalInput > 0){
               cantGoRight = true; 
            }
            if (horizontalInput < 0){
                cantGoLeft = true;
            }
        }
    }
}
