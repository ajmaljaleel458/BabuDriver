using System.Collections.Generic;
using UnityEditor;

namespace BabuDriver.MissionSystem
{
    public enum MissionState
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    public abstract class Mission
    {
        public event System.Action OnObjectiveUpdated; // Event for UI updates

        public event System.Action OnMissionComplete;

        public string Name { get; protected set; }
        public MissionState State { get; protected set; } = MissionState.NotStarted;
        protected List<Objective> objectives = new();
        private int currentObjectiveIndex = 0;

        public virtual void StartMission()
        {
            if (objectives.Count > 0)
            {
                State = MissionState.InProgress;
                objectives[currentObjectiveIndex].StartObjective();
            }
        }

        public virtual void UpdateMissionProgress()
        {
            if (State != MissionState.InProgress) return;

            Objective currentObjective = objectives[currentObjectiveIndex];
            currentObjective.UpdateObjective();

            if (currentObjective.IsFailed())
            {
                FailMission();
                return;
            }

            if (currentObjective.IsComplete())
            {
                AdvanceToNextObjective();
                OnObjectiveUpdated?.Invoke(); // Notify UIs
            }
        }

        private void AdvanceToNextObjective()
        {
            objectives[currentObjectiveIndex].EndObjective(true);

            if (++currentObjectiveIndex < objectives.Count)
            {
                objectives[currentObjectiveIndex].StartObjective();
            }
            else
            {
                CompleteMission();
                OnMissionComplete?.Invoke();
            }
        }

        protected virtual void CompleteMission() => State = MissionState.Completed;
        protected virtual void FailMission() => State = MissionState.Failed;
        public bool IsComplete() => State == MissionState.Completed;
        public virtual void EndMission() { }

        public List<Objective> GetObjectives() => objectives;
    }
}