namespace BabuDriver.VehicleDrivingSystem
{
    using UnityEngine;

    public class DrivingState : VehicleState
    {
        public override void Handle(VehicleController vehicle)
        {
            Debug.Log("Vehicle is driving.");
        }

        public override void Accelerate(VehicleController vehicle, float input)
        {
            vehicle.ApplyThrottle(input);
        }

        public override void Brake(VehicleController vehicle, float input)
        {
            vehicle.SetState(new BrakingState());
        }

        public override void Turn(VehicleController vehicle, float input)
        {
            vehicle.ApplySteering(input);
        }

        public override void Reverse(VehicleController vehicle, float input)
        {
            vehicle.SetState(new NeutralState());
        }

        public override void UpdateState(VehicleController vehicle)
        {
            // Check if there's no acceleration input and switch to neutral state
            if (Input.GetAxis("Vertical") == 0)
            {
                vehicle.SetState(new NeutralState());
            }
        }
    }
}