using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuView : MonoBehaviour
{
    public Button ExitButton;
    public Button SettingsButton;
    public Button GoToMissionSelectionButton;

    public void Initialize(VisualElement root)
    {
        ExitButton = root.Q<Button>("ExitButton");
        SettingsButton = root.Q<Button>("SettingsButton");
        GoToMissionSelectionButton = root.Q<Button>("GoToMissionSelectionButton");
    }
}
