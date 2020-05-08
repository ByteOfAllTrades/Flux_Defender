using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumping_enemy_controller : MonoBehaviour
{
    public float health = 100;
    public float damage = 10;
    public float attackBuffer = 2;
    public float jumpHeight = 20;
    public float jumpDelay = 6;

    Rigidbody2D rig;
    float damageBuffer;
    bool canJump = true;
    bool canAttack = true;
    bool canTakeDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        //remove this in prod
        PlayerPrefs.SetFloat("damageBuffer", 3);
        damageBuffer = PlayerPrefs.GetFloat("damageBuffer");
        rig = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canJump == true)
        {
            rig.AddForce(new Vector2(0, jumpHeight));
            canJump = false;
            Debug.Log("hop");
            StartCoroutine("jumpWithDelay");
        }
    }
    IEnumerator jumpWithDelay()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
        Debug.Log("skip");
    }
}
