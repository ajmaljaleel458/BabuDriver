using BabuDriver.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace BabuDriver.MissionSystem.Missions
{
    public sealed class ReachWarehouseObjective : Objective
    {
        private ObjectivePopupManager objectivePopupManager;

        public ReachWarehouseObjective(ObjectivePopupManager objectivePopupManager) 
            => this.objectivePopupManager = objectivePopupManager;

        public override void StartObjective()
        {
            GameEventManager.Subscribe("WarehouseEntered", OnWarehouseEntered);
            objectivePopupManager.PushObjectivePopup("Pick Up Product", "Go to Where House and pickup the product.");
        }

        private void OnWarehouseEntered(object eventData)
        {
            isEnded = true;
            Debug.Log("Product Collected then delever to buyer!");
            EndObjective(true);
        }

        public override void UpdateObjective()
        {
            
        }

        public override void EndObjective(bool isEnded)
        {
            this.isEnded = isEnded;
            GameEventManager.Unsubscribe("WarehouseEntered", OnWarehouseEntered);
        }

        public override bool IsComplete() => isEnded;
    }


    public sealed class DeliverToBuyerObjective : Objective
    {
        private ObjectivePopupManager objectivePopupManager;

        public DeliverToBuyerObjective(ObjectivePopupManager objectivePopupManager)
            => this.objectivePopupManager = objectivePopupManager;
        public override void StartObjective()
        {
            GameEventManager.Subscribe("DeleverProduct", OnBuyerApproached);
            objectivePopupManager.PushObjectivePopup("Deliver Product", "Go to buyer location and deliver.");
        }

        private void OnBuyerApproached(object eventData)
        {
            isEnded = true;
            Debug.Log("Product Collected by the buyer.");
            EndObjective(true);
        }

        public override void UpdateObjective()
        {
            
        }

        public override bool IsComplete() => isEnded;

        public override void EndObjective(bool isEnded)
        {
            this.isEnded = isEnded;
        }
    }

    public class DeliveryMission : Mission
    {
        private List<Objective> objectives = new List<Objective>();
        private int currentObjectiveIndex = 0;

        private ObjectivePopupManager objectivePopupManager;

        public DeliveryMission(string name, ObjectivePopupManager objectivePopupManager)
        {
            this.name = name;
            this.objectivePopupManager = objectivePopupManager;
        }

        public override void StartMission()
        {

            objectives.Add(new ReachWarehouseObjective(objectivePopupManager));
            objectives.Add(new DeliverToBuyerObjective(objectivePopupManager));

            if (objectives.Count > 0)
            {
                objectives[currentObjectiveIndex].StartObjective();
            }
        }

        public override void UpdateMissionProgress()
        {
            if (isComplete || isFailed) return;


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
            }
        }

        private void AdvanceToNextObjective()
        {
            if (currentObjectiveIndex < objectives.Count - 1)
            {
                objectives[currentObjectiveIndex].EndObjective(true);
                currentObjectiveIndex++;
                objectives[currentObjectiveIndex].StartObjective();
            }
            else
            {
                CompleteMission();
            }
        }

        private void CompleteMission()
        {
            isComplete = true;
        }

        private void FailMission()
        {
            isFailed = true;
        }

        public override bool IsComplete() => isComplete && !isFailed;

        public override void EndMission()
        {
        }
    }
}
