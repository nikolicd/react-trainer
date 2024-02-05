using UnityEngine;

[CreateAssetMenu(
    fileName = "SelectionSettings",
    menuName = "ScriptableObjects/SelectionSettings",
    order = 1
)]
public class SelectionSettings : ScriptableObject
{
    public string selectedOption;
    public int selectedLevel;
    public int selectedMin = 1;
    public int selectedSec = 0;
}
