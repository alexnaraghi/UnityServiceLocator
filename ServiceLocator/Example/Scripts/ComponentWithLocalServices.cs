using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorExamples
{
    /// <summary>
    /// We can create as many service locators as we'd like. Access local service locators directly, rather than through
    /// the static "Services" class.
    /// </summary>
    public class ComponentWithLocalServices : MonoBehaviour
    {
        public ServiceLocator localServices;
        public List<Transform> childTransforms;
        public float degreesPerSecond = 30f;

        private void Awake()
        {
            localServices.AddExisting(childTransforms);
        }

        private void Update()
        {
            var childTransforms = localServices.Get<List<Transform>>();

            foreach (var t in childTransforms)
            {
                t.RotateAround(transform.position, transform.up, degreesPerSecond * Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            localServices.Remove<Transform>();
        }
    }
}