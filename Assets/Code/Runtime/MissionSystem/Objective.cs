namespace BabuDriver.MissionSystem
{
    public abstract class Objective
    {
        public bool IsEnded { get; protected set; }
        public abstract void StartObjective();
        public abstract void UpdateObjective();
        public abstract void EndObjective(bool isEnded);
        public abstract bool IsComplete();
        public virtual bool IsFailed() => false;
        public abstract string GetDescription();
    }
}