using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_controller : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip coinSound;
    public AudioClip potionSound;
    public AudioClip bossCoinSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                PlayerPrefs.SetInt("potionTotal", (PlayerPrefs.GetInt("potionTotal")+1));

        }
    }
}
