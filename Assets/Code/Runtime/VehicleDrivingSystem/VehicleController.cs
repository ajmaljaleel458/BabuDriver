using UnityEngine;

namespace BabuDriver.VehicleDrivingSystem
{
    public enum Gear
    {
        Neutral = 0,
        Drive,
        Reverse,
        Park
    }
    public class VehicleController : MonoBehaviour
    {
        [Header("Car Properties")]
        public float motorTorque = 2000f;
        public float brakeTorque = 2000f;
        public float maxSpeed = 20f;
        public float steeringRange = 30f;
        public float steeringRangeAtMaxSpeed = 10f;
        public float centreOfGravityOffset = -1f;

        public WheelController[] wheels;

        public Rigidbody rigidBody;
        private VehicleState currentState;

        public Gear currentGear = Gear.Neutral;

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            wheels = GetComponentsInChildren<WheelController>();

            // Adjust center of mass for better stability
            Vector3 centerOfMass = rigidBody.centerOfMass;
            centerOfMass.y += centreOfGravityOffset;
            rigidBody.centerOfMass = centerOfMass;

            // Set default state to Neutral
            SetState(new NeutralState());
        }

        public void SetState(VehicleState newState)
        {
            currentState = newState;
            currentState.Handle(this);
        }

        private void FixedUpdate()
        {
            float vInput = Input.GetAxis("Vertical");
            float hInput = Input.GetAxis("Horizontal");

            if (vInput > 0)
            {
                currentState.Accelerate(this, vInput);
            }
            else if (vInput < 0)
            {
                currentState.Reverse(this, vInput);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                currentState.Brake(this, 1f);
            }

            if (hInput != 0)
            {
                currentState.Turn(this, hInput);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                currentState.Park(this);
            }

            currentState.UpdateState(this);
        }

        public void ApplyThrottle(float input)
        {
            float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));
            float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

            foreach (var wheel in wheels)
            {
                if (wheel.motorized)
                {
                    wheel.WheelCollider.motorTorque = input * currentMotorTorque;
                }
                wheel.WheelCollider.brakeTorque = 0f;
            }
        }

        public void ApplyBrakes(float input)
        {
            foreach (var wheel in wheels)
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = input * brakeTorque;
            }
        }

        public void ApplySteering(float input)
        {
            float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
            float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));
            float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

            foreach (var wheel in wheels)
            {
                if (wheel.steerable)
                {
                    wheel.WheelCollider.steerAngle = input * currentSteerRange;
                }
            }
        }
    }
}