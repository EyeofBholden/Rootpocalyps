using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Prefab references.
    public GameObject chainedExplosion;

    public int destructionTimer;
    public float scaleFactor;
    public AudioSource source;
    public AudioClip explosionSound;
    void Start()
    {
        destructionTimer = 25;
        scaleFactor = Random.Range(2.0f, 2.75f);
        gameObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);
        source.PlayOneShot(explosionSound, 0.07f);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (destructionTimer > 0) destructionTimer -= 1;

        if (destructionTimer == 0)
        {
            Destroy(gameObject, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        Debug.Log(other.name.ToString());
        if (other.CompareTag("root") || other.CompareTag("bombRoot"))
        {
            //if (other.CompareTag("bombRoot"))
            //{
            //    Instantiate(chainedExplosion, other.transform.position, Quaternion.identity);
            //}

            Destroy(other, 0.0f);
        }
    }
}
