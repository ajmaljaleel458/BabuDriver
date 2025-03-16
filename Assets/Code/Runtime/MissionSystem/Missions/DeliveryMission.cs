using BabuDriver.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace BabuDriver.MissionSystem.Missions
{
    public sealed class ReachWarehouseObjective : Objective
    {
        public override void StartObjective()
        {
            GameEventManager.Subscribe("WarehouseEntered", OnWarehouseEntered);
        }

        private void OnWarehouseEntered(object eventData)
        {
            isEnded = true;
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
        public override void StartObjective()
        {
        }

        public override void UpdateObjective()
        {
            if (PlayerHasDeliveredToBuyer())
            {
                EndObjective(true);
            }
        }

        public override bool IsComplete() => isEnded;

        public override void EndObjective(bool isEnded)
        {
            this.isEnded = isEnded;
        }

        private bool PlayerHasDeliveredToBuyer()
        {
            // Implement logic to check if player has reached the buyer
            return true; // Placeholder
        }
    }

    public class DeliveryMission : Mission
    {
        private List<Objective> objectives = new List<Objective>();
        private int currentObjectiveIndex = 0;

        public DeliveryMission(string name) => this.name = name;

        public override void StartMission()
        {

            objectives.Add(new ReachWarehouseObjective());
            objectives.Add(new DeliverToBuyerObjective());

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
