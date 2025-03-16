namespace BabuDriver.VehicleDrivingSystem
{
    using UnityEngine;

    public class NeutralState : VehicleState
    {
        public override void Handle(VehicleController vehicle)
        {
            Debug.Log("Vehicle is in Neutral.");
        }

        public override void Accelerate(VehicleController vehicle, float input)
        {
            vehicle.SetState(new DrivingState());
        }

        public override void Turn(VehicleController vehicle, float input)
        {
            vehicle.ApplySteering(input);
        }

        public override void Reverse(VehicleController vehicle, float input)
        {
            vehicle.SetState(new ReverseState());
        }

        public override void Park(VehicleController vehicle)
        {
            // Switch to ParkingState if vehicle is stopped
            if (vehicle.rigidBody.linearVelocity.magnitude < 0.1f && vehicle.currentGear != Gear.Park)
            {
                vehicle.SetState(new ParkingState());
            }
            else
            {
                Debug.Log("Cannot switch to Parking while moving.");
            }
        }

        public override void UpdateState(VehicleController vehicle)
        {
            // Set motor torque to zero
            foreach (var wheel in vehicle.wheels)
            {
                wheel.WheelCollider.motorTorque = 0f;
            }

            Vector3 velocity = vehicle.rigidBody.linearVelocity;

            // Apply rolling resistance
            vehicle.rigidBody.linearVelocity *= 0.98f;

            // Stop the vehicle completely when it's almost stopped and on flat ground
            if (velocity.magnitude < 0.1f && Mathf.Abs(vehicle.transform.forward.y) < 0.1f)
            {
                vehicle.rigidBody.linearVelocity = Vector3.zero;
                vehicle.rigidBody.angularVelocity = Vector3.zero;
            }
        }
    }
}