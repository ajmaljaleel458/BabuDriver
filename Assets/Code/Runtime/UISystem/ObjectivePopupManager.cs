using UnityEngine;
using UnityEngine.UIElements;

public class ObjectivePopupManager : MonoBehaviour
{
    public UIDocument uIDocument;

    private Label objectiveTitle;

    private Label objectiveDiscription;

    private VisualElement PopupBox;

    private void Start()
    {
        var root = uIDocument.rootVisualElement;

        PopupBox = root.Q<VisualElement>("popup-box");

        objectiveTitle = PopupBox.Q<Label>("title");

        objectiveDiscription = PopupBox.Q<Label>("discription");
    }

    public void PushObjectivePopup(string title, string discription)
    {
        objectiveTitle.text = title;
        objectiveDiscription.text = discription;
        PopupBox.style.display = DisplayStyle.Flex;
    }

    public void ClearObjectivePopup()
    {
        PopupBox.style.display = DisplayStyle.None;
    }
}
