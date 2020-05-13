using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spectre_controller : MonoBehaviour
{
    private bool dirRight = true;
    public float speed = 20f;
    Transform Target;
    // Start is called before the first frame update
    void Start()
    {
         Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            this.transform.localScale =  new Vector3(-6,6,0);
             transform.Translate (Vector2.right * speed * Time.deltaTime);
        }
             
         else
         {
              this.transform.localScale =  new Vector3(6,6,0);
             transform.Translate (-Vector2.right * speed * Time.deltaTime);
         }
             
         
         if(transform.position.x >= Target.position.x) 
         {
             dirRight = false;
             
         }
         
         if(transform.position.x <= Target.position.x) 
         {
             dirRight = true;
         }
    }
}
