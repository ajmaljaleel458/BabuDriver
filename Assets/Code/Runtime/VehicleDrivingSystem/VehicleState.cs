namespace BabuDriver.VehicleDrivingSystem
{
    public abstract class VehicleState
    {
        public abstract void Handle(VehicleController vehicle);
        public virtual void Accelerate(VehicleController vehicle, float input) { }
        public virtual void Brake(VehicleController vehicle, float input) { }
        public virtual void Turn(VehicleController vehicle, float input) { }
        public virtual void Reverse(VehicleController vehicle, float input) { }
        public virtual void Park(VehicleController vehicle) { }
        public virtual void UpdateState(VehicleController vehicle) { }
    }
}