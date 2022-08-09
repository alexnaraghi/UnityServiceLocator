using UnityEngine;
using UnityEngine.UI;

namespace ServiceLocatorExamples
{
    /// <summary>
    /// Accesses the achievement service and game state.
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        public Slider spinSlider;
        public Button completeAchievementButton;
        public Button logServiceLocatorButton;
        public Button increaseScoreButton;
        public Button addServiceButton;
        public Button removeServiceButton;
        public Text serviceLocatorText;
        public string achievementName;

        private IAchievementService _achievementService;
        private GameState _gameState;

        private void Awake()
        {
            // We can either cache access to a service or access it on demand.
            _achievementService = Services.Get<IAchievementService>();
            _gameState = Services.Get<GameState>();

            spinSlider.onValueChanged.AddListener(OnCameraRotationChanged);
            completeAchievementButton.onClick.AddListener(OnCompleteAchievementPressed);
            logServiceLocatorButton.onClick.AddListener(OnLogServiceLocatorPressed);
            increaseScoreButton.onClick.AddListener(IncreaseScorePressed);
            addServiceButton.onClick.AddListener(OnAddService);
            removeServiceButton.onClick.AddListener(OnRemoveService);
            
            // The add and remove service buttons swap being visible when they are pressed.
            removeServiceButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            spinSlider.onValueChanged.RemoveListener(OnCameraRotationChanged);
            completeAchievementButton.onClick.RemoveListener(OnCompleteAchievementPressed);
            completeAchievementButton.onClick.RemoveListener(OnLogServiceLocatorPressed);
            increaseScoreButton.onClick.RemoveListener(IncreaseScorePressed);
            addServiceButton.onClick.RemoveListener(OnAddService);
            removeServiceButton.onClick.RemoveListener(OnRemoveService);
        }
        
        private void OnCameraRotationChanged(float value)
        {
            var cameraManager = Services.Get<CameraManager>();

            cameraManager.degreesPerSecond = value;
        }

        private void OnCompleteAchievementPressed()
        {
            _achievementService.CompleteAchievement(achievementName);
        }

        private void IncreaseScorePressed()
        {
            _gameState.score++;
        }

        private void OnLogServiceLocatorPressed()
        {
            Debug.Log(Services.instance);
        }
        
        private void OnAddService()
        {
            // Add the service if it's not already added.
            if (Services.Get<GameUI>() == null)
            {
                Services.AddExisting(this);
            }
            
            addServiceButton.gameObject.SetActive(false);
            removeServiceButton.gameObject.SetActive(true);

            serviceLocatorText.text = Services.instance.ToString();
        }
        
        private void OnRemoveService()
        {
            // Remove automatically destroys the component. If we don't want that, we can use RemoveWithoutDestroying.
            Services.RemoveWithoutDestroying<GameUI>();
            
            addServiceButton.gameObject.SetActive(true);
            removeServiceButton.gameObject.SetActive(false);
            
            serviceLocatorText.text = Services.instance.ToString();
        }
    }
}
