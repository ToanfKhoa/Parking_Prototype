using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float realTime;
    public float initTime = 30f;
    public int level = 1;
    public int countBlock = 0;
    public int countSlot = 0;
    public int levelUpDifficult = 1;
    public int timeBonus;

    public TMP_Text timeBonusText;
    public TMP_Text levelText;
    public TMP_Text countBlockText;
    public TMP_Text countSlotText;
    public TMP_Text levelUpDifficultText;
    public Slider timeSlider;

    public GameObject panel;

    public bool isGameOver = false;

    private void Start()
    {
        realTime = initTime;
        timeSlider.maxValue = initTime;  
        timeSlider.value = realTime;     
    }

    private void Update()
    {
        if (realTime > 0)
        {
            realTime -= Time.deltaTime;
            timeSlider.value = realTime;

        }

        UpdateDisplay();
        
        if(isGameOver)
            panel.SetActive(true);
    }

    void UpdateDisplay()
    {
        timeBonusText.text = "Time Bonus: " + timeBonus.ToString();
        levelText.text = "Level: " + level.ToString();
        countBlockText.text = "Count Block: " + countBlock.ToString();
        countSlotText.text = "Count Slot: " + countSlot.ToString();
        levelUpDifficultText.text = "Change Difficulty Every: " + levelUpDifficult.ToString() + " Level";
    }

    public void ResetRealTime()
    {
        realTime = initTime;
    }

    public void IncreaseLevel()
    {
        level++;
    }

    public void DecreaseLevel()
    {
        if (level > 0) level--;
    }

    public void IncreaseCountBlock()
    {
        countBlock++;
    }

    public void DecreaseCountBlock()
    {
        if (countBlock > 0) countBlock--;
    }

    public void IncreaseCountSlot()
    {
        countSlot++;
    }

    public void DecreaseCountSlot()
    {
        if (countSlot > 0) countSlot--;
    }

    public void IncreaseLevelUpDifficulty()
    {
        levelUpDifficult++;
    }

    public void DecreaseLevelUpDifficulty()
    {
        if (levelUpDifficult > 0) levelUpDifficult--;
    }

    public void IncreaseTimeBonus()
    {
        timeBonus++;
    }

    public void DecreaseTimeBonus()
    {
        timeBonus--;
    }

    public void IncreaseRealTime(float amount)
    {
        realTime += amount;
    }

    public void OpenPanel()
    {
        panel.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }    

    public void Play()
    {
        realTime = initTime;
        timeSlider.maxValue = initTime;
        timeSlider.value = realTime;
        isGameOver = false;
    }
}

