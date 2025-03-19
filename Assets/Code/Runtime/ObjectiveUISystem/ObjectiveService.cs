using UnityEngine;

namespace BabuDriver.ObjectiveUISystem
{
    public class ObjectiveService : MonoBehaviour
    {
        public static ObjectiveService Instance { get; private set; }
        private ObjectiveController objectiveController;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            objectiveController = FindFirstObjectByType<ObjectiveController>();
        }

        public void PushObjective(string title, string description)
        {
            objectiveController.SetNewObjective(title, description);
        }

        public void CompleteObjective()
        {
            objectiveController.RemoveObjective();
        }
    }
}