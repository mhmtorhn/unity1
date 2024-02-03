using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Experience : MonoBehaviour
{
    public Image expIMG;

    public Text levelText;
    public int currentLevel;

    public float currentExperience;
    public float expToNextLevel;

    public static Experience instance;

    public AudioSource levelUpAS;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        expIMG.fillAmount = currentExperience / expToNextLevel;
        currentLevel = 1;
        levelText.text=currentLevel.ToString();

        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    
    void Update()
    {
        expIMG.fillAmount = currentExperience / expToNextLevel;
        levelText.text = currentLevel.ToString();
    }

    public void expMod(float experience)
    {
        
        currentExperience += experience;

        
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);


        expIMG.fillAmount = currentExperience/expToNextLevel;

        if(currentExperience >= expToNextLevel)
        {
            expToNextLevel *= 2;
            currentExperience = 0;
            currentLevel++;
            levelText.text = currentLevel.ToString();
            PlayerHealth.instance.maxHealth += 5;
            PlayerHealth.instance.currentHealth += 5;
           
            AudioManager.instance.PlayAudio(levelUpAS);
           

            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);

        }

        

    }

    public void DataSave()
    {
        DataManager.instance.ExperienceData(currentExperience);
        DataManager.instance.ExperienceToNextLevel(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);

        DataManager.instance.CurrentHealth(PlayerHealth.instance.currentHealth);
        PlayerHealth.instance.currentHealth = PlayerPrefs.GetFloat("CurrentHealth");

        DataManager.instance.MaxHealth(PlayerHealth.instance.maxHealth);
        PlayerHealth.instance.maxHealth = PlayerPrefs.GetFloat("MaxHealth");

        currentExperience = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        DataManager.instance.CurrentStar(StarBank.instance.bankStar);
        StarBank.instance.bankStar = PlayerPrefs.GetInt("StarAmount");

    }


}
