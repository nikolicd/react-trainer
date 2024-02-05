using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Reference to the ScriptableObject
    public SelectionSettings selectionSettings;

    // Main Menu Buttons
    public List<Button> menuButtons;

    // Navigation Buttons
    public Button backButton;
    public Button startButton;
    public Button exitButton;
    public Button continueButton;

    private void Start()
    {
        // Adding Methods to Menu Buttons
        if (menuButtons.Count > 0)
        {
            for (int i = 0; i < menuButtons.Count; i++)
            {
                int index = i;

                menuButtons[index]
                    .onClick
                    .AddListener(() =>
                    {
                        // Selecting Option
                        selectionSettings.selectedOption = string.Format(
                            $"{menuButtons[index].GetComponentInChildren<TMP_Text>().text}"
                        );
                        // Loading Selected Scene
                        SceneManager.LoadScene("OptionsScene");
                    });
            }
        }

        // Go to Selection Scene
        if (continueButton != null)
        {
            continueButton
                .onClick
                .AddListener(() =>
                {
                    SceneManager.LoadScene("SelectionScene");
                });
        }

        // Back to Selection Scene
        if (backButton != null)
        {
            backButton
                .onClick
                .AddListener(() =>
                {
                    SceneManager.LoadScene("SelectionScene");
                });
        }

        // Starting the Game
        if (startButton != null)
        {
            startButton
                .onClick
                .AddListener(() =>
                {
                    SceneManager.LoadScene("GameScene");
                });
        }

        // Exiting the Game
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    // Navigation
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
