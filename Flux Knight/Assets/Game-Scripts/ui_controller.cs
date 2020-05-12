using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ui_controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame()
    {
        Debug.Log("Click");
        SceneManager.LoadScene("startArea");
        PlayerPrefs.SetFloat("currentEnergy", 0);
        PlayerPrefs.SetFloat("currentHealth", 100);
        PlayerPrefs.SetString("popMsg", "");
        PlayerPrefs.SetInt("coinTotal", 100);
        PlayerPrefs.SetString("levelTitle", "Default Dungeon");
        PlayerPrefs.SetInt("potionTotal", 0);
        PlayerPrefs.SetInt("potionLimit", 5);
        //PlayerPrefs.SetFloat("damageBuffer", 0.15f);
        PlayerPrefs.SetFloat("playerKnockback", 100);
        PlayerPrefs.SetFloat("playerDamage", 10);
        PlayerPrefs.SetFloat("challengeModifier", 1);
    }
    public void Quit()
    {
        Debug.Log("Quit");
        //Uncomment before build
        //Application.Quit();
    }
}
