using UnityEngine;
using UnityEngine.UIElements;

namespace BabuDriver.ObjectiveUISystem
{
    public class ObjectiveView : MonoBehaviour
    {
        private Label titleLabel;
        private Label descriptionLabel;
        private VisualElement root;

        private void OnEnable()
        {
            var uiDocument = GetComponent<UIDocument>();
            root = uiDocument.rootVisualElement;

            titleLabel = root.Q<Label>("objective-title");
            descriptionLabel = root.Q<Label>("objective-description");
        }

        public void UpdateObjective(ObjectiveData objective)
        {
            titleLabel.text = objective.Title;
            descriptionLabel.text = objective.Description;
        }

        public void ClearObjective()
        {
            titleLabel.text = "";
            descriptionLabel.text = "";
        }
    }
}
