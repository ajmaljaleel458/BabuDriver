namespace BabuDriver.MissionSystem
{
    public abstract class Objective
    {
        protected bool isEnded;
        public abstract void StartObjective();
        public abstract void UpdateObjective();
        public abstract void EndObjective(bool isEnded);
        public abstract bool IsComplete();
        public virtual bool IsFailed() => false;  // Default behavior: Not failed
    }
}