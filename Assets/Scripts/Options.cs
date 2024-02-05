using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    // Selection Images
    public Image imagePanel;
    public Sprite colors;
    public Sprite numbers;
    public Sprite letters;
    public Sprite lcLeft;
    public Sprite lcRight;

    // Reference to the ScriptableObject
    public SelectionSettings selectionSettings;
    public TextMeshProUGUI sceneTitle;

    void Start()
    {
        // Updating the page title
        sceneTitle.text = selectionSettings.selectedOption;

        // Updating the image
        switch (selectionSettings.selectedOption)
        {
            case "Colors":
                imagePanel.sprite = colors;
                break;
            case "Numbers":
                imagePanel.sprite = numbers;
                break;
            case "Letters":
                imagePanel.sprite = letters;
                break;
            case "Letters & Colors Left Side":
                imagePanel.sprite = lcLeft;
                break;
            case "Letters & Colors Right Side":
                imagePanel.sprite = lcRight;
                break;
        }

        // Reseting Level & Duration
        selectionSettings.selectedLevel = 0;
        selectionSettings.selectedMin = 1;
        selectionSettings.selectedSec = 0;
    }
}
