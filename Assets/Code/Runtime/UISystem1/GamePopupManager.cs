using UnityEngine;
using UnityEngine.UIElements;

namespace BabuDriver.UISystem
{
    public enum GamePopupType
    {
        MissionComplete,
        MissionFailed
    }

    public class GamePopupManager : MonoBehaviour
    {
        private VisualElement popupContainer;
        private Label popupTitle;
        private Button homeButton;
        private Button replayButton;

        public void ShowPopup(GamePopupType popupType)
        {
            popupContainer.style.display = DisplayStyle.Flex;

            switch (popupType)
            {
                case GamePopupType.MissionComplete:
                    popupTitle.text = "Mission Completed!";
                    break;
                case GamePopupType.MissionFailed:
                    popupTitle.text = "Mission Failed!";
                    break;
            }
        }

        public void HidePopup()
        {
            popupContainer.style.display = DisplayStyle.None;
        }
    }
}