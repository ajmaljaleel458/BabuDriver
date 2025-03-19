using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BabuDriver.UISystem
{
    public enum LobbyUIState
    {
        MainMenu,
        LevelSelection,
        Reward
    }

    public class LobbyUIManager : MonoBehaviour
    {
        public UIDocument uIDocument;

        private VisualElement mainMenu;
        private VisualElement levelMenu;

        private Button getStartedButton;

        private void Start()
        {
            var root = uIDocument.rootVisualElement;

            mainMenu = root.Q<VisualElement>("main-menu");

            levelMenu = root.Q<VisualElement>("level-seletion");

            getStartedButton = mainMenu.Q<Button>("start-bitton");

            getStartedButton.clicked += () => SceneManager.LoadScene(1);
        }

        public void SwitchToMenu(LobbyUIState newState)
        {
            mainMenu.style.display = DisplayStyle.None;
            levelMenu.style.display = DisplayStyle.None;

            switch (newState)
            {
                case LobbyUIState.MainMenu:
                    mainMenu.style.display = DisplayStyle.Flex;
                    break;
                case LobbyUIState.LevelSelection:
                    levelMenu.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}