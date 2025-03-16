namespace BabuDriver.MissionSystem
{
    using UnityEngine;
    using System.Collections.Generic;
    using Missions;
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance;

        private Mission currentMission;
        private List<Mission> availableMissions; // This list can hold all your available missions
        private bool missionInProgress;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            availableMissions = new List<Mission>();
            missionInProgress = false;

            AddMission(new DeliveryMission("DeliveryMission"));

            StartMission(availableMissions[0]);
        }

        void Update()
        {
            if (missionInProgress && currentMission != null)
            {
                currentMission.UpdateMissionProgress();

                // Check if the current mission is complete
                if (currentMission.IsComplete())
                {
                    EndMission();
                }
            }
        }

        // Start a new mission
        public void StartMission(Mission mission)
        {
            if (missionInProgress)
            {
                return;
            }

            currentMission = mission;
            missionInProgress = true;

            // Initialize mission
            currentMission.StartMission();

            // Update UI with mission info (you can call a UI Manager here)
            //UIManager.Instance.UpdateMissionUI(currentMission);
        }

        // End the current mission
        public void EndMission()
        {
            if (currentMission == null)
                return;

            missionInProgress = false;

            // Mission success or failure
            if (currentMission.IsComplete())
            {
                //RewardPlayer();  // Call a reward system here
            }
            else
            {
            }

            // End mission and transition to next mission or reset
            TransitionToNextMission();
        }

        // Reward the player after completing a mission
        //private void RewardPlayer()
        //{
        //    // Add points, unlockables, or other rewards here
        //    RewardManager.Instance.GiveReward(currentMission);
        //}

        // Transition to the next mission
        private void TransitionToNextMission()
        {
            // Implement logic for transitioning to the next mission, or reset the current mission
            // For example, load the next mission in a sequence

            // Example: Load the next mission from the list (if available)
            if (availableMissions.Count > 0)
            {
                StartMission(availableMissions[0]);
                availableMissions.RemoveAt(0);
            }
            else
            {
                Debug.Log("No more missions available!");
                // End of available missions, reset or load the main menu
            }
        }

        // Add mission to available list
        public void AddMission(Mission mission)
        {
            availableMissions.Add(mission);
        }

        // Get the current mission
        public Mission GetCurrentMission()
        {
            return currentMission;
        }

        // Check if a mission is currently in progress
        public bool IsMissionInProgress()
        {
            return missionInProgress;
        }
    }
}