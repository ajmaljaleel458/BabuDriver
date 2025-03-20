using BabuDriver.GameEvents;
using System.Collections.Generic;
using UnityEngine;

namespace BabuDriver.MissionSystem.Missions
{
    public sealed class ReachWarehouseObjective : Objective
    {
        public override string GetDescription() => "Go to the warehouse and pick up the product.";

        public override void StartObjective()
        {
            GameEventManager.Subscribe("WarehouseEntered", OnWarehouseEntered);
        }

        private void OnWarehouseEntered(object eventData)
        {
            EndObjective(true);
        }

        public override void EndObjective(bool isEnded)
        {
            this.IsEnded = isEnded;
            GameEventManager.Unsubscribe("WarehouseEntered", OnWarehouseEntered);
        }

        public override void UpdateObjective() { }

        public override bool IsComplete() => IsEnded;
    }

    public sealed class DeliverToBuyerObjective : Objective
    {
        public override string GetDescription() => "Go to the buyer's location and deliver the product.";

        public override void StartObjective()
        {
            GameEventManager.Subscribe("DeliverProduct", OnBuyerApproached);
        }

        private void OnBuyerApproached(object eventData)
        {
            EndObjective(true);
        }

        public override void EndObjective(bool isEnded)
        {
            this.IsEnded = isEnded;
            GameEventManager.Unsubscribe("DeliverProduct", OnBuyerApproached);
        }

        public override void UpdateObjective() { }

        public override bool IsComplete() => IsEnded;
    }

    public sealed class DeliveryMission : Mission
    {

        public DeliveryMission(string name)
        {
            this.Name = name;
            InitializeObjectives();
        }

        private void InitializeObjectives()
        {
            objectives.Add(new ReachWarehouseObjective());
            objectives.Add(new DeliverToBuyerObjective());
        }

        public override void StartMission()
        {
            base.StartMission();
        }
    }
}
