using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moleDeath : MonoBehaviour
{
    public string deathScene;
    public AudioSource scource;
    public AudioClip deathSound;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("root"))
        {
            StartCoroutine(death());
        }
    }
    IEnumerator death(){
            scource.PlayOneShot(deathSound, 0.35f);
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(deathScene);
        
    }
}
