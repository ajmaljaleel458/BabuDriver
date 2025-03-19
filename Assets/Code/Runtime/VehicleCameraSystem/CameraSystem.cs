using UnityEngine;
using UnityEngine.UI;

namespace BabuDriver.VehicleCameraSystem
{
    public enum CameraMode
    {
        Orbital,
        Cockpit,
        Lobby
        // You can add more modes later here
    }

    public class CameraSystem : MonoBehaviour
    {
        public static CameraSystem Instance;
        public CameraMode currentMode = CameraMode.Lobby;
        //public Text cameraModeText; // UI text to display the current camera mode
        public GameObject orbitalCamObj; // Reference to the orbital camera
        public GameObject cockpitCamObj; // Reference to the cockpit camera
        public GameObject lobbyCamObj;
        private CameraMode lastMode;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            SetCameraMode(currentMode); // Initialize with default camera mode
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) // Switch camera mode on pressing 'C'
            {
                SwitchCameraMode();
            }
        }

        public void SwitchCameraMode()
        {
            lastMode = currentMode;
            currentMode = (CameraMode)(((int)currentMode + 1) % System.Enum.GetValues(typeof(CameraMode)).Length); // Cycle through modes
            SetCameraMode(currentMode);
        }

        public void SetCameraMode(CameraMode mode)
        {
            // Disable all cameras
            orbitalCamObj.SetActive(false);
            cockpitCamObj.SetActive(false);
            lobbyCamObj.SetActive(false);

            switch (mode)
            {
                case CameraMode.Orbital:
                    orbitalCamObj.SetActive(true);
                    break;
                case CameraMode.Cockpit:
                    cockpitCamObj.SetActive(true);
                    break;
                case CameraMode.Lobby:
                    lobbyCamObj.SetActive(true);
                    break;
            }

            // Update the UI text to indicate current camera mode
            //cameraModeText.text = "Camera Mode: " + mode.ToString();
        }
    }
}