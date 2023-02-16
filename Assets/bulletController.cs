using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletController : MonoBehaviour
{
    // Prefab references.
    public GameObject explosion;

    // Other members.
    public float force;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, rot - 62);
    }

    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.name == "PlayArea")
        {
            Destroy(gameObject, 1);    
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (other.CompareTag("root") || other.CompareTag("bombRoot"))
        {
            if (other.CompareTag("bombRoot"))
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(other, 0.0f);
            Destroy(gameObject, 0.0f);
        }
    }
}
