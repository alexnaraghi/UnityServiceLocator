using System;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

/// <summary>
/// Singleton global service locator, lazy instantiated.
/// </summary>
public static class Services
{
    private static ServiceLocator _instance;
    private const string MonoBehaviourName = "_ServiceLocator";

    /// <summary>
    /// Singleton access w/lazy instantiation.  Creates a game object to hold mono-behaviour based services if one does 
    /// not exist.
    /// </summary>
    public static ServiceLocator instance
    {
        get
        {
            if (_instance == null)
            {
#if DEBUG
				StackTrace stackTrace = new StackTrace();

				for (int i = 0; i < stackTrace.FrameCount; ++i)
				{
					StackFrame frame = stackTrace.GetFrame(i);
					MethodBase methodBase = frame.GetMethod();

					if (methodBase.Name.Contains("OnDestroy") || methodBase.Name.Contains("OnDisable"))
					{
						Debug.Log($"A new ServiceLocator was instantiated in an OnDisable/OnDestroy method. Use \"Services.exists\" to avoid creating a new ServiceLocator while quitting the game.");
					}
				}
#endif

	            var go = new GameObject(MonoBehaviourName);
				Object.DontDestroyOnLoad(go);
				
				_instance = go.AddComponent<ServiceLocator>();

                AddGlobalBindings(_instance);
            }
            
            return _instance;
        }
    }

    /// <summary>
    /// Check for when a service is requested during OnDisable or OnDestroy.
    /// Not needed for standard use, just when calling a service while cleaning up a MonoBehaviour, since the 
    /// service locator itself could be destroyed (Unity destroys objects in a non-deterministic order).
    /// </summary>
    public static bool exists => _instance != null;

    // Syntactic sugar so we don't have to qualify static access calls with "Instance".
    public static T Get<T>() => instance.Get<T>();
    public static T Add<T>() => instance.Add<T>();
    public static T Add<T, U>() where U : T => instance.Add<T, U>();
    public static void Remove<T>() => instance.Remove<T>();
    public static void Remove(Type type) => instance.Remove(type);
    public static void RemoveWithoutDestroying<T>() => instance.RemoveWithoutDestroying<T>();
    public static void RemoveWithoutDestroying(Type type) => instance.RemoveWithoutDestroying(type);
    public static void AddExisting<T>(T existing) => instance.AddExisting(existing);
    public static void AddExisting<T, U>(U existing) where U : T => instance.AddExisting<T, U>(existing);

    // Adds game-specific global bindings.
    private static void AddGlobalBindings(ServiceLocator serviceLocator)
    {
    }
}