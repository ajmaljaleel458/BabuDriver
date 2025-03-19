using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPresenter
{
    private MainMenuView _view;

    public MainMenuPresenter(MainMenuView view)
    {
        _view = view;
        _view.ExitButton.clicked += OnExitClicked;
        _view.GoToMissionSelectionButton.clicked += OnMissionSelectionClicked;
    }

    private void OnExitClicked()
    {
        Application.Quit();
    }

    private void OnMissionSelectionClicked()
    {
        SceneManager.LoadScene("MissionSelectionScene");
    }
}
