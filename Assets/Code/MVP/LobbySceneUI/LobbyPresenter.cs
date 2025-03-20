using UnityEngine;

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
        _view.ToActualGameButton.clicked += () => Debug.Log("Call Mission manager");
    }

    private void ChangeMenu(MenuModel.MenuType menu)
    {
        _model.SetMenu(menu);
        _view.ShowMenu(menu);
    }
}