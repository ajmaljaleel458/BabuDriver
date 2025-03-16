namespace BabuDriver.VehicleDrivingSystem
{ 
    using UnityEngine;
    public class ParkingState : VehicleState
    {
        public override void Handle(VehicleController vehicle)
        {
            Debug.Log("Vehicle is in Park.");
            vehicle.currentGear = Gear.Park;

            // Set motor torque to zero when parked
            foreach (var wheel in vehicle.wheels)
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = 1000f;  // Optional: apply brake torque to simulate the vehicle staying still
            }
        }

        public override void Park(VehicleController vehicle)
        {
            // Switch back to NeutralState when in Park and the player presses P
            vehicle.SetState(new NeutralState());
        }

        public override void UpdateState(VehicleController vehicle)
        {
            // The vehicle is parked, so no movement will occur
            foreach (var wheel in vehicle.wheels)
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = 1000f;  // Apply a braking torque to prevent movement
            }
        }
    }
}