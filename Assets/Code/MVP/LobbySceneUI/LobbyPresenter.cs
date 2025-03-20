using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyPresenter
{
    private LobbyView _view;
    private MenuModel _model;

    public LobbyPresenter(LobbyView view)
    {
        _view = view;
        _model = new MenuModel();

        _view.ToMissionMenuButton.clicked += () => ChangeMenu(MenuModel.MenuType.Mission);
        _view.BackToHomeFromMissionMenu.clicked += () => ChangeMenu(MenuModel.MenuType.Home);
        _view.ToActualGameButton.clicked += () => SceneManager.LoadScene(1);
    }

    private void ChangeMenu(MenuModel.MenuType menu)
    {
        _model.SetMenu(menu);
        _view.ShowMenu(menu);
    }
}