using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Controller : MonoBehaviour
{
    public Slider healthBar;
    public Slider energyBar;
    public Text coinTotal;
    public Text popMsg;
    public Text levelTitle;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        energyBar =  GameObject.Find("EnergyBar").GetComponent<Slider>();
        coinTotal = GameObject.Find("coinTotal").GetComponent<Text>();
        popMsg = GameObject.Find("popMsg").GetComponent<Text>();
        levelTitle = GameObject.Find("levelTitle").GetComponent<Text>();
        PlayerPrefs.SetFloat("currentEnergy", 20);
        PlayerPrefs.SetFloat("currentHealth", 20);
        PlayerPrefs.SetString("popMsg", "Test test");
        PlayerPrefs.SetInt("coinTotal", 100);
        PlayerPrefs.SetString("levelTitle", "Default Dungeon");
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = (1 / PlayerPrefs.GetFloat("maxHealth")) * PlayerPrefs.GetFloat("currentHealth");
        energyBar.value = 0.01f * PlayerPrefs.GetFloat("currentEnergy");
        coinTotal.text = PlayerPrefs.GetInt("coinTotal").ToString();
        popMsg.text = PlayerPrefs.GetString("popMsg");
        levelTitle.text = PlayerPrefs.GetString("levelTitle");
    }
}
