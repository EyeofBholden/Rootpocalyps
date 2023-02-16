using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float fireRate;
    public AudioSource scource;
    public AudioClip pew;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (!canFire){
            timer += Time.deltaTime;
            if (timer > fireRate){
                canFire = true;
                timer = 0;
            }
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput > 0){ // Character travelling right
            gameObject.transform.localScale = new Vector3(-1,1,1);
        } else if(horizontalInput <0) {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        if (Input.GetMouseButton(0) && canFire){
            // bullet = rnd.Next(3);
            canFire = false;
            scource.PlayOneShot(pew, 0.1f);
            Instantiate(Bullet1, bulletTransform.position, Quaternion.identity);
        }
    }
}
