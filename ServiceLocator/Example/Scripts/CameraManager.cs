using UnityEngine;

namespace ServiceLocatorExamples
{
    /// <summary>
    /// Rotates the camera based on rotation speed.
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        public float degreesPerSecond = 0f;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Services.Get<Camera>();
        }

        private void Update()
        {
            _camera.transform.RotateAround(Vector3.zero, Vector3.up, degreesPerSecond * Time.deltaTime);
        }
    }
}