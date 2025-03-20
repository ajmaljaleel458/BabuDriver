using BabuDriver.MissionSystem;
using UnityEngine.SceneManagement;


public class MissionPresenter
{
    private readonly MissionModel model;
    private readonly MissionView view;

    public MissionPresenter(MissionView view)
    {
        this.view = view;
        model = new MissionModel();

        MissionManager.Instance.GetCurrentMission().OnObjectiveUpdated += UpdateUI;
        MissionManager.Instance.GetCurrentMission().OnMissionComplete += () => SceneManager.LoadScene(0);
    }

    public void UpdateUI()
    {
        Mission currentMission = MissionManager.Instance.GetCurrentMission();
        if (currentMission == null) return;

        model.MissionTitle = currentMission.Name;
        model.Objectives.Clear();

        foreach (Objective objective in currentMission.GetObjectives())
        {
            model.Objectives.Add(new ObjectiveModel
            {
                Description = objective.GetDescription(),
                IsCompleted = objective.IsComplete()
            });
        }

        view.Render(model);
    }
}