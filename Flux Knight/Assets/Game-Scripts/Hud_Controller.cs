using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Controller : MonoBehaviour
{
    //Declare Public Variables
    public Slider healthBar;
    public Slider energyBar;
    public Text coinTotal;
    public Text popMsg;
    public Text levelTitle;
    public Text potionTotal;
    // Start is called before the first frame update
    void Start()
    {
        //Get components for each of the UI items
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        energyBar =  GameObject.Find("EnergyBar").GetComponent<Slider>();
        coinTotal = GameObject.Find("coinTotal").GetComponent<Text>();
        popMsg = GameObject.Find("popMsg").GetComponent<Text>();
        levelTitle = GameObject.Find("levelTitle").GetComponent<Text>();
        potionTotal = GameObject.Find("potionTotal").GetComponent<Text>();

        //test the functionality of the UI
        PlayerPrefs.SetFloat("currentEnergy", 20);
        PlayerPrefs.SetFloat("currentHealth", 20);
        PlayerPrefs.SetString("popMsg", "Test test");
        PlayerPrefs.SetInt("coinTotal", 100);
        PlayerPrefs.SetString("levelTitle", "Default Dungeon");
        PlayerPrefs.SetInt("potionTotal", 3);
    }

    // Update is called once per frame
    void Update()
    {
        //Set all of the values to the appropriate display
        healthBar.value = (1 / PlayerPrefs.GetFloat("maxHealth")) * PlayerPrefs.GetFloat("currentHealth");
        energyBar.value = 0.01f * PlayerPrefs.GetFloat("currentEnergy");
        coinTotal.text = PlayerPrefs.GetInt("coinTotal").ToString();
        popMsg.text = PlayerPrefs.GetString("popMsg");
        levelTitle.text = PlayerPrefs.GetString("levelTitle");
        potionTotal.text = PlayerPrefs.GetInt("potionTotal").ToString();
    }
}
