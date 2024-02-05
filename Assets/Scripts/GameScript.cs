using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Displayed Item Class
[System.Serializable]
public class Item
{
    //[FormerlySerializedAs("itemType1")]
    public ItemType itemType;
    public ItemSide itemSide;
    public Color itemColor;
    public AudioClip itemAudio;
    public string itemText;

    public enum ItemType
    {
        Color,
        Number,
        Letter
    }

    public enum ItemSide
    {
        Left,
        Right,
        None
    }
}

public class GameScript : MonoBehaviour
{
    // Item Data
    public Image itemPanel;
    public TMP_Text itemText;
    public AudioSource itemAudio;
    private int itemIndex;

    // Item Lists
    //[FormerlySerializedAs("panelData")]
    public List<Item> itemData;
    private List<Item> colorItems;
    private List<Item> numberItems;
    private List<Item> letterItems;
    private List<Item> lcLeftItems;
    private List<Item> lcRightItems;
    private List<Item> activeItems;

    // Reference to the ScriptableObject
    public SelectionSettings selectionSettings;

    // Stop Button
    public bool gameStoped = true;

    // Timer
    public TMP_Text timeText;
    private float duration;
    private float timePassed = 0;
    private float interval;

    // Score
    public GameObject scorePanel;
    public TMP_Text scoreText;

    // Stop Button
    public Button stopButton;

    //    private void OnValidate()
    //    {
    //        foreach (var item in itemData)
    //        {
    //            switch (item.itemType)
    //            {
    //                case "color":
    //                    item.itemType1 = Item.ItemType.Color;
    //                    break;
    //                case "number":
    //                    item.itemType1 = Item.ItemType.Number;
    //                    break;
    //                case "letter":
    //                    item.itemType1 = Item.ItemType.Letter;
    //                    break;
    //                case "lcLeft":
    //                    item.itemType1 = Item.ItemType.LcLeft;
    //                    break;
    //                case "lcRight":
    //                    item.itemType1 = Item.ItemType.LcRight;
    //                    break;
    //            }
    //        }
    //#if UNITY_EDITOR
    //        UnityEditor.EditorUtility.SetDirty(this);
    //#endif
    //    }

    void Start()
    {
        // Hidding Score Panel
        scorePanel.SetActive(false);

        // Setting Duration
        duration = 60 * selectionSettings.selectedMin + selectionSettings.selectedSec;

        // Starting Game
        gameStoped = false;

        // Setting Lists
        colorItems = itemData.Where(item => item.itemType == Item.ItemType.Color).ToList();
        numberItems = itemData.Where(item => item.itemType == Item.ItemType.Number).ToList();
        letterItems = itemData.Where(item => item.itemType == Item.ItemType.Letter).ToList();
        lcLeftItems = itemData.Where(item => item.itemSide == Item.ItemSide.Left).ToList();
        lcRightItems = itemData.Where(item => item.itemSide == Item.ItemSide.Right).ToList();

        // Setting Active Items
        switch (selectionSettings.selectedOption)
        {
            case "Colors":
                activeItems = colorItems;
                break;
            case "Numbers":
                activeItems = numberItems;
                break;
            case "Letters":
                activeItems = letterItems;
                break;
            case "Letters & Colors Left Side":
                activeItems = lcLeftItems;
                break;
            case "Letters & Colors Right Side":
                activeItems = lcRightItems;
                break;
        }

        SetIndex(activeItems);

        // Display Items
        StartCoroutine(ChangeItemData());

        // Stopping the game
        stopButton.onClick.AddListener(() => StopGame());
    }

    void Update()
    {
        // Setting Interval
        interval = 12 - selectionSettings.selectedLevel * 2;

        // Updating Time and Score
        if (!gameStoped)
        {
            if (timePassed < duration - 1)
            {
                timePassed += Time.deltaTime;
                UpdateTime(timePassed);
                UpdateScore();
            }
            else
            {
                gameStoped = true;
            }
        }
        else
        {
            scorePanel.SetActive(true);
        }
    }

    // Setting Item Starting Index
    void SetIndex(List<Item> data)
    {
        itemIndex = Random.Range(0, data.Count);
    }

    // Updating Item
    IEnumerator ChangeItemData()
    {
        while (!gameStoped)
        {
            Item currentItem = activeItems[itemIndex];

            itemPanel.color = currentItem.itemColor;
            itemText.text = currentItem.itemText;
            itemAudio.clip = currentItem.itemAudio;
            itemAudio.Play();

            yield return new WaitForSeconds(interval);

            SetIndex(activeItems);
        }
    }

    // Updating Duration
    void UpdateTime(float timeToUpdate)
    {
        timeToUpdate += 1;

        float minutes = Mathf.FloorToInt(timeToUpdate / 60);
        float seconds = Mathf.FloorToInt(timeToUpdate % 60);

        timeText.text = string.Format($"{minutes:00}:{seconds:00}");
    }

    // Updating Score
    void UpdateScore()
    {
        float score = Mathf.FloorToInt(timePassed / interval + 1);

        scoreText.text = string.Format($"{score}");
    }

    // Stopping the game
    void StopGame()
    {
        gameStoped = true;
    }
}
