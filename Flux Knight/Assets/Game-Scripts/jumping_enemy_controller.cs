using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumping_enemy_controller : MonoBehaviour
{
    //enemy settings
    [Header("Attribute Settings")]
    public float baseHealth = 100;
    public float baseDamage = 10;
    public float attackBuffer = 2;
    public float jumpHeight = 20;
    public float jumpDelay = 6;
    public float energyBonus = 10;

    //audio settings
    
    AudioSource audioSource;
    [Header("Audio Settings")]
    public AudioClip hitSound;
    public AudioClip deathSound;

    //loot settings
    [Header("Loot Settings")]
    public GameObject coin;
    public GameObject potion;

    public int maxCoins;
    public int minCoins;
    public float potionChance;

    Rigidbody2D rig;
    float damageBuffer;
    bool canJump = true;
    bool isGrounded = false;
    bool canAttack = true;
    bool canTakeDamage = true;
    bool isDead = false;
    float health;
    float damage;
    Vector2 deathPos;
    Vector2 lootSpawnPos;
    int coinDrop;
    float potionRoll;
    int soundBuffer;
    // Start is called before the first frame update
    void Start()
    {
        //remove this in prod
        //PlayerPrefs.SetFloat("damageBuffer", 0.15f);
        PlayerPrefs.SetFloat("playerKnockback", 100);
        PlayerPrefs.SetFloat("playerDamage", 10);
        PlayerPrefs.SetFloat("challengeModifier", 1);
        audioSource = GetComponent<AudioSource>();
        damageBuffer = PlayerPrefs.GetFloat("damageBuffer");
        rig = gameObject.GetComponent<Rigidbody2D>();
        health = baseHealth * PlayerPrefs.GetFloat("challengeModifier");
        damage = baseDamage * PlayerPrefs.GetFloat("challengeModifier");
        soundBuffer = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetFloat("portalHealed") == 1)
        {
            if (soundBuffer == 0)
            {
                soundBuffer++;
                StartCoroutine("Die");
            }
            
        }
        
        if (isGrounded)
        {
            if (canJump)
        {
            rig.AddForce(new Vector2(0, jumpHeight));
            canJump = false;
            isGrounded = false;
            Debug.Log("hop");
            StartCoroutine("jumpWithDelay");
        }
        }
        if (health <= 0 && isDead == false)
        {
           
           Debug.Log("Iam Dead");
           StartCoroutine("Die");

        }
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "platform")
        {
            isGrounded = true;
        }
        if (col.gameObject.tag == "Player")
        {
            if (canAttack)
            {
                PlayerPrefs.SetFloat("currentHealth", (PlayerPrefs.GetFloat("currentHealth") - damage));
                Debug.Log("boom");
                StartCoroutine("startAttackBuffer");
            }
            
            
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "weapon")
        {
            if (canTakeDamage)
            {
                if (this.transform.localScale.x == -1)
                {
                    health -= PlayerPrefs.GetFloat("playerDamage");
                    rig.AddForce(new Vector2((PlayerPrefs.GetFloat("playerKnockback")* -1),(PlayerPrefs.GetFloat("playerKnockback")* -1)));
                    
                }
                if (this.transform.localScale.x == 1)
                {
                    health -= PlayerPrefs.GetFloat("playerDamage");
                    rig.AddForce(new Vector2(PlayerPrefs.GetFloat("playerKnockback"),PlayerPrefs.GetFloat("playerKnockback")));
                }
                audioSource.clip = hitSound;
                audioSource.Play();
                //StartCoroutine("startDamageBuffer");
                
            }
            
        }
    }
    void SpawnLoot()
    {
        coinDrop = UnityEngine.Random.Range(minCoins,maxCoins);
        potionRoll = UnityEngine.Random.Range(0f,1f);
        if (potionRoll < potionChance)
        {
            Instantiate(potion, deathPos, Quaternion.identity);
        }
        for(int i = 0; i < coinDrop; i++)
        {
            lootSpawnPos.x = UnityEngine.Random.Range((deathPos.x -6),(deathPos.x +6));
            lootSpawnPos.y = deathPos.y;
            Instantiate(coin, lootSpawnPos, Quaternion.identity);
        }
        
    }
    IEnumerator Die()
    {
        isDead = true;
        audioSource.clip = deathSound;
        audioSource.Play();  
        yield return new WaitForSeconds(1);
        deathPos = this.transform.position;
        SpawnLoot();
        PlayerPrefs.SetFloat("spawnedEnemies",  PlayerPrefs.GetFloat("spawnedEnemies")-1);
        PlayerPrefs.SetFloat("currentEnergy", PlayerPrefs.GetFloat("currentEnergy")+energyBonus);
        Destroy(this.gameObject);
    }
    IEnumerator jumpWithDelay()
    {
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
        Debug.Log("skip");
    }
    IEnumerator startDamageBuffer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageBuffer);
        canTakeDamage = true;
    }
    IEnumerator startAttackBuffer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackBuffer);
        canAttack = true;
    }
}
