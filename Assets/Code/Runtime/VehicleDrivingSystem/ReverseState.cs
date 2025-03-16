namespace BabuDriver.VehicleDrivingSystem
{
    using UnityEngine;

    public class ReverseState : VehicleState
    {
        public override void Handle(VehicleController vehicle)
        {
        }

        public override void Accelerate(VehicleController vehicle, float input)
        {
        }

        public override void Brake(VehicleController vehicle, float input)
        {
            vehicle.ApplyBrakes(input);
        }

        public override void Reverse(VehicleController vehicle, float input)
        {
            // Apply reverse throttle
            ReverseAcceleration(vehicle, -input);
        }

        public override void UpdateState(VehicleController vehicle)
        {
            // Check if there's no acceleration input and switch to neutral state
            if (Input.GetAxis("Vertical") == 0)
            {
                vehicle.SetState(new NeutralState());
            }
        }

        public override void Turn(VehicleController vehicle, float input)
        {
            vehicle.ApplySteering(input);
        }

        private void ReverseAcceleration(VehicleController vehicle, float input)
        {
            // Get the current forward speed
            float forwardSpeed = Vector3.Dot(vehicle.transform.forward, vehicle.rigidBody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, vehicle.maxSpeed, Mathf.Abs(forwardSpeed));

            // Calculate current motor torque (ensure it's in reverse direction)
            float currentMotorTorque = Mathf.Lerp(vehicle.motorTorque, 0, speedFactor);

            // Apply negative torque for reverse movement
            foreach (var wheel in vehicle.wheels)
            {
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = -input * currentMotorTorque; // Apply negative torque for reverse
                }
                wheel.WheelCollider.brakeTorque = 0f; // Make sure brakes are not applied when accelerating in reverse
            }
        }
    }
}