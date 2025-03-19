using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using BabuDriver.MissionSystem;
using BabuDriver.VehicleCameraSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject LobbyControllPanel;
    public GameObject InGameControllPanel;

    public Button startGameButton;

    public Button settingsButton;
    public Button restartMissionButton;
    public Button goToLobbyButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Assign button actions
        startGameButton.onClick.AddListener(StartGame);
        //settingsButton.onClick.AddListener(ToggleSettingsMenu);
        //restartMissionButton.onClick.AddListener(RestartMission);
        //goToLobbyButton.onClick.AddListener(GoToLobby);
    }

    private void StartGame()
    {
        CameraSystem.Instance.SetCameraMode(CameraMode.Orbital);
        MissionManager.Instance.StartMission(MissionManager.Instance.availableMissions[0]);
        LobbyControllPanel.SetActive(false);
    }

    //private void ToggleSettingsMenu()
    //{
    //    settingsMenu.SetActive(!settingsMenu.activeSelf);
    //}

    //private void RestartMission()
    //{
    //    GameManager.Instance.RestartMission();
    //}

    //private void GoToLobby()
    //{
    //    SceneManager.LoadScene("LobbyScene");
    //}
}