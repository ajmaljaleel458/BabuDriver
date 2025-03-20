using UnityEngine;
using UnityEngine.UIElements;

public class MissionView : MonoBehaviour
{
    private UIDocument UIDocument;

    private MissionPresenter _presenter;

    public VisualElement Root;
    public VisualElement ObjectivesWindow, ObjectiveList;
    public Label MissionTitle;

    private VisualTreeAsset objectiveTemplate;

    public void Start()
    {
        UIDocument = GetComponent<UIDocument>();

        Root = UIDocument.rootVisualElement;

        ObjectivesWindow = Root.Q<VisualElement>("ObjectivesWindow");
        ObjectivesWindow.style.display = DisplayStyle.None;

        ObjectiveList = Root.Q<VisualElement>("ObjectiveList");
        objectiveTemplate = Resources.Load<VisualTreeAsset>("ObjectiveItem");
            
        MissionTitle = Root.Q<Label>("MissionTitle");

        _presenter = new MissionPresenter(this);

        _presenter.UpdateUI();
    }

    public void Render(MissionModel model)
    {
        ObjectivesWindow.style.display = DisplayStyle.Flex;

        ObjectiveList.Clear();

        MissionTitle.text = model.MissionTitle;

        foreach (var objective in model.Objectives)
        {
            var newObjectiveItem = objectiveTemplate.Instantiate();
            newObjectiveItem.Q<Label>("ObjectiveDiscription").text = objective.Description;
            newObjectiveItem.Q<VisualElement>("ObjectiveState").style.backgroundColor = objective.IsCompleted ? Color.green : Color.gray;

            ObjectiveList.Add(newObjectiveItem);
        }
    }
}
