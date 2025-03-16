namespace BabuDriver.VehicleDrivingSystem
{
    using UnityEngine;

    public class BrakingState : VehicleState
    {
        public override void Handle(VehicleController vehicle)
        {
            Debug.Log("Vehicle is braking.");
        }

        public override void Accelerate(VehicleController vehicle, float input)
        {
            vehicle.SetState(new DrivingState());
        }

        public override void Brake(VehicleController vehicle, float input)
        {
            vehicle.ApplyBrakes(input);
        }
    }
}