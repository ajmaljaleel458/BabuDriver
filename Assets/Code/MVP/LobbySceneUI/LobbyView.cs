using UnityEngine;
using UnityEngine.UIElements;

public class LobbyView : MonoBehaviour
{
    private UIDocument UIDocument;

    private LobbyPresenter _presenter;

    public VisualElement Root;
    public VisualElement HomeMenu, MissionMenu;
    public Button ToMissionMenuButton, BackToHomeFromMissionMenu, ToActualGameButton;

    public Label MenuTitle;

    public void Start()
    {
        UIDocument = GetComponent<UIDocument>();

        Root = UIDocument.rootVisualElement;

        MenuTitle = Root.Q<Label>("MenuTitle");

        HomeMenu = Root.Q<VisualElement>("HomeMenu");
        MissionMenu = Root.Q<VisualElement>("MissionMenu");

        ToMissionMenuButton = Root.Q<Button>("ToMissionMenuBtn");
        BackToHomeFromMissionMenu = Root.Q<Button>("BackToHomeFromSettings");
        ToActualGameButton = Root.Q<Button>("ToActualGame");

        _presenter = new LobbyPresenter(this);

        ShowMenu(MenuModel.MenuType.Home);
    }

    public void ShowMenu(MenuModel.MenuType menu)
    {
        HomeMenu.style.display = (menu == MenuModel.MenuType.Home) ? DisplayStyle.Flex : DisplayStyle.None;
        MissionMenu.style.display = (menu == MenuModel.MenuType.Mission) ? DisplayStyle.Flex : DisplayStyle.None;
    }
}