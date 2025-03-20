namespace BabuDriver.MissionSystem
{
    using UnityEngine;
    using System.Collections.Generic;
    using BabuDriver.MissionSystem.Missions;

    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance { get; private set; }
        private Mission currentMission;
        private readonly List<Mission> availableMissions = new();
        private bool missionInProgress;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            AddMission(new DeliveryMission("Delivery Misssion"));

            StartMission(availableMissions[0]);
        }

        private void Update()
        {
            if (missionInProgress && currentMission != null)
            {
                currentMission.UpdateMissionProgress();
                if (currentMission.IsComplete()) EndMission();
            }
        }

        public void StartMission(Mission mission)
        {
            if (missionInProgress) return;

            currentMission = mission;
            missionInProgress = true;
            currentMission.StartMission();
        }

        public void EndMission()
        {
            if (currentMission == null) return;
            missionInProgress = false;

            if (currentMission.IsComplete())
            {
                // Reward system logic here
            }
            TransitionToNextMission();
        }

        private void TransitionToNextMission()
        {
            if (availableMissions.Count > 0)
            {
                StartMission(availableMissions[0]);
                availableMissions.RemoveAt(0);
            }
            else
            {
                Debug.Log("No more missions available!");
            }
        }

        public void AddMission(Mission mission) => availableMissions.Add(mission);
        public Mission GetCurrentMission() => currentMission;
        public bool IsMissionInProgress() => missionInProgress;
    }
}