using UnityEngine;

namespace BabuDriver.ObjectiveUISystem
{
    public class ObjectiveController : MonoBehaviour
    {
        private ObjectiveView objectiveView;

        private void Awake()
        {
            objectiveView = GetComponent<ObjectiveView>();
        }

        public void SetNewObjective(string title, string description)
        {
            var objective = new ObjectiveData(title, description);
            objectiveView.UpdateObjective(objective);
        }

        public void RemoveObjective()
        {
            objectiveView.ClearObjective();
        }
    }
}