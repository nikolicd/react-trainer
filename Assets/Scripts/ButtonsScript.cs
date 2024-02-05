using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    // Reference to the ScriptableObject
    public SelectionSettings selectionSettings;

    // Colors Buttons
    private Color normalButtonColor = Color.white;
    private Color selectedButtonColor = Color.black;

    // Level Selection Buttons
    public List<Button> levelButtons;

    // Duration Dropdown Buttons
    public TMP_Dropdown minDropdown;
    public TMP_Dropdown secDropdown;

    // Start Button
    public Button startButton;

    void Start()
    {
        // Adding Listners to Level Buttons
        foreach (var button in levelButtons)
        {
            SetButtonColor(button, normalButtonColor);
            button.onClick.AddListener(() => ButtonClicked(button));
        }

        // Adding Listners to Duration Dropdown Buttons
        minDropdown.onValueChanged.AddListener(OnMinSelected);
        secDropdown.onValueChanged.AddListener(OnSecSelected);

        // Setting Duration Dropdown Button default Values
        minDropdown.value = selectionSettings.selectedMin - 1;

        if (selectionSettings.selectedSec == 0)
        {
            secDropdown.value = 0;
        }
        else
        {
            secDropdown.value = 1;
        }
    }

    private void Update()
    {
        // Enabling Start Button if level is selected
        if (selectionSettings.selectedLevel < 1)
        {
            startButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
        }

        // Disabling Sec Dropdown if Minutes are set to 10
        if (selectionSettings.selectedMin == 10)
        {
            selectionSettings.selectedSec = 0;
            secDropdown.value = 0;
            secDropdown.interactable = false;
        }
        else
        {
            secDropdown.interactable = true;
        }
    }

    // Button Click Event
    void ButtonClicked(Button clickedButton)
    {
        foreach (var button in levelButtons)
        {
            SetButtonColor(
                button,
                button == clickedButton ? selectedButtonColor : normalButtonColor
            );

            if (button == clickedButton)
            {
                string selectedValueString = button.tag;

                if (int.TryParse(selectedValueString, out int selectedValue))
                {
                    selectionSettings.selectedLevel = selectedValue;
                }
            }
        }
    }

    // Updating Button Color
    void SetButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        cb.selectedColor = color;
        button.colors = cb;
    }

    // Updating Minutes Duration
    public void OnMinSelected(int index)
    {
        string selectedValueString = minDropdown.options[index].text;

        if (int.TryParse(selectedValueString, out int selectedValue))
        {
            selectionSettings.selectedMin = selectedValue;
        }
        else
        {
            Debug.LogError("Selected value is not a valid integer: " + selectedValueString);
        }
    }

    // Updating Seconds Duration
    public void OnSecSelected(int index)
    {
        string selectedValueString = secDropdown.options[index].text;

        if (int.TryParse(selectedValueString, out int selectedValue))
        {
            selectionSettings.selectedSec = selectedValue;
        }
        else
        {
            Debug.LogError("Selected value is not a valid integer: " + selectedValueString);
        }
    }
}
