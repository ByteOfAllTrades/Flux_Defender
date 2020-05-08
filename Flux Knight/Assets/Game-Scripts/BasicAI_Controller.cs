using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI_Controller : MonoBehaviour
{
    private bool dirRight = true;
    public float speed = 20f;
    public Transform Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            this.transform.localScale =  new Vector3(-1,1,0);
             transform.Translate (Vector2.right * speed * Time.deltaTime);
        }
             
         else
         {
              this.transform.localScale =  new Vector3(1,1,0);
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

        // if (Vector3.Distance(transform.position, Player.position) >= MinDist)
        // {
        //     transform.position += transform.forward * MoveSpeed * Time.deltaTime;


        //     if (Vector3.Distance(transform.position, Player.position) <= MaxDist)
        //     {

        //     }
        // }
    }
}
