using System.Collections.Generic;

namespace BabuDriver.MissionSystem
{
    public abstract class Mission
    {
        public bool isComplete;
        public bool isFailed;

        public string name;
        public abstract void StartMission();  // Starts the mission
        public abstract void UpdateMissionProgress();  // Updates mission progress
        public abstract bool IsComplete();  // Checks if the mission is complete
        public abstract void EndMission();  // Ends the mission and handles rewards

        public override string ToString()
        {
            return name;
        }
    }
}