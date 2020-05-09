using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal_controller : MonoBehaviour
{
    //spawn controls
    public GameObject[] Enemies;
    public GameObject portalHeart;
    public Sprite corruptedPortalHeart;
    public Sprite friendlyPortalHeart;
    public float MaxSpawnDelay = 3;
    public float MinSpawnDelay = 1;

    bool canSpawn = true;
    int portalState = 1;
    int enemy;
    float spawnDelay;
    Vector2 spawnHeart;
    float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        portalState = 1;
        portalHeart.GetComponent<SpriteRenderer>().sprite = corruptedPortalHeart;
        spawnHeart = portalHeart.transform.position;
        spawnHeart.y = spawnHeart.y -3.2f;
        PlayerPrefs.SetFloat("spawnedEnemies",0);
        PlayerPrefs.SetFloat("portalHealed", 0);        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerPrefs.GetFloat("currentEnergy") >= 100)
            {
               Debug.Log("portal open");
               PlayerPrefs.SetFloat("currentEnergy", 0);
               PlayerPrefs.SetFloat("portalHealed", 1);
               portalState = 0;
            
            }
        enemyCount = PlayerPrefs.GetFloat("spawnedEnemies");
        if (portalState == 1)
        {
            if (canSpawn)
            {
                if (enemyCount < PlayerPrefs.GetFloat("challengeModifier"))
                {
                    spawnRandomEnemy();
                    StartCoroutine("startSpawnDelay");
                    PlayerPrefs.SetFloat("spawnedEnemies",  PlayerPrefs.GetFloat("spawnedEnemies")+1);
                }
                
            }

        }
        else if (portalState == 0)
        {
            portalHeart.GetComponent<SpriteRenderer>().sprite = friendlyPortalHeart;
        }
        
    }
    void spawnRandomEnemy()
    {
         enemy = UnityEngine.Random.Range(0,(Enemies.Length - 1));
         Instantiate(Enemies[enemy], spawnHeart, Quaternion.identity);
    }
    IEnumerator startSpawnDelay()
    {
        canSpawn = false;
        spawnDelay = UnityEngine.Random.Range(MinSpawnDelay,MaxSpawnDelay);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
