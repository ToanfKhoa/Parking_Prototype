using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion


    public float realTime;
    public float initTime = 30f;
    public int level = 1;
    public int countBlock = 0;
    public int countSlot = 4;
    public int timeBonus;

    public TMP_Text timeBonusText;
    public TMP_Text levelText;
    public TMP_Text countBlockText;
    public TMP_Text countSlotText;
    public TMP_Text initTimeText;
    public Slider timeSlider;

    public GameObject panel;

    public bool isGameOver = false;

    public List<GameObject> parkingSlots = new List<GameObject>();

    public GameObject player;

    public Collider2D randomSlotCollider;

    private void Start()
    {
        realTime = initTime;
        timeSlider.maxValue = initTime;  
        timeSlider.value = realTime;
        Play();
    }

    private void Update()
    {
        if (realTime > 0)
        {
            realTime -= Time.deltaTime;
            timeSlider.value = realTime;
        }
        else
            isGameOver = true;

        /*if (Input.GetKey(KeyCode.W))
            player.transform.localPosition += new Vector3(0, 1) * 5 * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            player.transform.localPosition += new Vector3(0, -1) * 5 * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            player.transform.localPosition += new Vector3(-1, 0) * 5 * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            player.transform.localPosition += new Vector3(1, 0) * 5 * Time.deltaTime;*/

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
        initTimeText.text = "Init Time: " + initTime.ToString();
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

    public void IncreaseInitTime()
    {
        initTime += 5;
    }

    public void DecreaseInitTime()
    {
        if (initTime > 0) initTime -= 5;
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
        level = 1;
     
        panel.SetActive(false);

        RandomMap();

        isGameOver = false;
    }

    public void NextLevel()
    {
        level++;
        if (countSlot > 1)
            countSlot--;
        if(countBlock < 5) 
            countBlock++;
        RandomMap();
    }

    public void RandomMap()
    {
        RemoveThing();
        RandomRemoveCarSLot();
        RandomBlock();
        RandomPlayerPosition();
        RandomClock();
    }
    public void RandomRemoveCarSLot()
    {
        foreach (GameObject slot in parkingSlots)
        {
            slot.SetActive(true);
        }

        for (int i = 0; i < parkingSlots.Count; i++)
        {
            int randomIndex = Random.Range(0, parkingSlots.Count);
            GameObject temp = parkingSlots[i];
            parkingSlots[i] = parkingSlots[randomIndex];
            parkingSlots[randomIndex] = temp;
        }

        for(int i = 0;i < countSlot;i++)
        {
            parkingSlots[i].SetActive(false);
        }
    }


    public List<GameObject> spawnPlayerpoints = new List<GameObject>();
    public void RandomPlayerPosition()
    {
        if (spawnPlayerpoints.Count > 0)
        {
            int i = Random.Range(0, spawnPlayerpoints.Count);
            player.transform.localPosition = spawnPlayerpoints[i].transform.localPosition;
        }        
    }

    public void Lose()
    {
        isGameOver = true;
        panel.SetActive(true);
    }

    public GameObject blockPrefab;
    public GameObject spawnBlockArea;
    public GameObject safeArea;
    public void RandomBlock()
    {
        Vector2 randomPosition;
        Vector2 rectA_Size = spawnBlockArea.transform.localScale; // Kích thước của spawnBlockArea
        Vector2 rectA_Center = spawnBlockArea.transform.position; // Tâm của spawnBlockArea

        Vector2 rectB_Size = safeArea.transform.localScale;       // Kích thước của safeArea
        Vector2 rectB_Center = safeArea.transform.position;       // Tâm của safeArea

        for (int i = 0; i < countBlock; i++)
        {
            do
            {
                // Tạo tọa độ ngẫu nhiên trong hình chữ nhật spawnBlockArea
                float x = Random.Range(rectA_Center.x - rectA_Size.x / 2, rectA_Center.x + rectA_Size.x / 2);
                float y = Random.Range(rectA_Center.y - rectA_Size.y / 2, rectA_Center.y + rectA_Size.y / 2);
                randomPosition = new Vector2(x, y);
            }
            while (IsInsideRectB(randomPosition, rectB_Center, rectB_Size)); // Lặp lại nếu vị trí nằm trong safeArea

            // Tạo block tại vị trí hợp lệ
            Instantiate(blockPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
        }
    }

    private bool IsInsideRectB(Vector2 position, Vector2 rectB_Center, Vector2 rectB_Size)
    {
        // Kiểm tra nếu vị trí nằm trong hình chữ nhật safeArea
        return position.x >= rectB_Center.x - rectB_Size.x / 2 &&
               position.x <= rectB_Center.x + rectB_Size.x / 2 &&
               position.y >= rectB_Center.y - rectB_Size.y / 2 &&
               position.y <= rectB_Center.y + rectB_Size.y / 2;
    }

    public void RemoveThing()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");
        if (blocks.Length > 0)
        {
            foreach (GameObject block in blocks)
            {
                Destroy(block);
            }
        }
        GameObject[] clocks = GameObject.FindGameObjectsWithTag("clock");
        if (blocks.Length > 0)
        {
            foreach (GameObject clock in clocks)
            {
                Destroy(clock);
            }
        }
    }

    public GameObject clockPrefab;
    public void RandomClock()
    {
        Vector2 randomPosition;
        Vector2 rectA_Size = spawnBlockArea.transform.localScale; // Kích thước của spawnBlockArea
        Vector2 rectA_Center = spawnBlockArea.transform.position; // Tâm của spawnBlockArea

        Vector2 rectB_Size = safeArea.transform.localScale;       // Kích thước của safeArea
        Vector2 rectB_Center = safeArea.transform.position;       // Tâm của safeArea

        do
        {
        float x = Random.Range(rectA_Center.x - rectA_Size.x / 2, rectA_Center.x + rectA_Size.x / 2);
        float y = Random.Range(rectA_Center.y - rectA_Size.y / 2, rectA_Center.y + rectA_Size.y / 2);
        randomPosition = new Vector2(x, y);
        }
        while (IsInsideRectB(randomPosition, rectB_Center, rectB_Size)); 

        Instantiate(clockPrefab, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
    }

}

