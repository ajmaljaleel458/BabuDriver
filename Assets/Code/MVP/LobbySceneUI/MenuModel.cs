public class MenuModel
{
    public enum MenuType { Home, Mission }
    public MenuType CurrentMenu { get; private set; }

    public void SetMenu(MenuType menu)
    {
        CurrentMenu = menu;
    }
}