using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio_controller : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip potionSound;
    public AudioClip bossCoinSound;
    public AudioClip clearSound;
    public AudioClip deathSound;
    
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E ) && PlayerPrefs.GetFloat("currentEnergy") >= 100)
        {
            Debug.Log("oof");
            audioSource.clip = clearSound;
            audioSource.Play();
        }
        if (PlayerPrefs.GetFloat("currentHealth") <= 0)
        {
            if (isDead == false)
            {
                audioSource.clip = deathSound;
                audioSource.Play();
                isDead = true;
            }
            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "coin")
        {
                audioSource.clip = coinSound;
                audioSource.Play();
                PlayerPrefs.SetInt("coinTotal", (PlayerPrefs.GetInt("coinTotal")+1));

        }
        if(col.gameObject.tag == "potion")
        {
                audioSource.clip = potionSound;
                audioSource.Play();
                if (PlayerPrefs.GetInt("potionTotal") < PlayerPrefs.GetInt("potionLimit"))
                {
                    PlayerPrefs.SetInt("potionTotal", (PlayerPrefs.GetInt("potionTotal")+1));
                }
                

        }
    }
}
