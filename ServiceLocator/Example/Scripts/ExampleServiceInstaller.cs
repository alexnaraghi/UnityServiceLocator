using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorExamples
{
    /// <summary>
    /// Example of a custom installer with a variety of different service installations.
    /// Inherit the service installer to have complete control over the order that services are added.
    /// Be sure to set its Script Execution Order to before your other scripts so you can access these services in the
    /// Awake() method.
    /// </summary>
    public class ExampleServiceInstaller : ServiceInstaller
    {
        public Camera mainCamera;
        public List<GameObject> gameObjects;

        protected override void Awake()
        {
            // We can add already existing mono-behaviours.
            Services.AddExisting(mainCamera);

            // We can add pure C# objects and generics.
            Services.AddExisting(gameObjects);

            // We can register an object as an interface, useful for platform-specific classes and unit tests.
#if UNITY_EDITOR
            Services.Add<IAchievementService, EditorAchievementService>();
#else
            Services.Add<IAchievementService, StandaloneAchievementService>();
#endif

            // We can create a new mono-behaviour on the service locator.
            var gameState = Services.Add<GameState>();
            gameState.StartGame();

            // We have full control over the order that services are installed so they can depend on each other.
            // The default services can be installed before or after our custom ones.
            base.Awake();
        }

        /// <summary>
        /// Clean up anything manually created in OnDestroy.
        /// </summary>
        protected override void OnDestroy()
        {
            // We make sure the service locator still exists in OnDestroy methods, since it may already be destroyed if 
            // we are shutting down the game.
            if (Services.exists)
            {
                // Removing a Unity component will destroy the component on the ServiceLocator's MonoBehaviour.
                Services.Remove<GameState>();

                // Removing a C# object will simply remove its reference.
                Services.Remove(gameObjects.GetType());

                // Remember to remove the interface and not the concrete type when you use a different binding type.
                Services.Remove<IAchievementService>();
            }

            // This will automatically cleanup any references created by the default inspector list.
            base.OnDestroy();
        }
    }
}
