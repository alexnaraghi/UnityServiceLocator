using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Minimal service locator container for Unity.
/// </summary>
public class ServiceLocator : MonoBehaviour
{
    /// <summary>
    /// The bindings of all services attached to the persistent game object.
    /// The type is the binding type and the MonoBehaviour is the concrete instance that it resolves to.
    /// </summary>
    private readonly Dictionary<Type, object> _serviceBindings = new Dictionary<Type, object>();

    /// <summary>
    /// Gets the service binding that resolves the given type.
    /// </summary>
    /// <returns>
    /// Null if the service does not exist.
    /// </returns>
    public T Get<T>()
    {
        return (T)Get(typeof(T));
    }

    public object Get(Type t)
    {
        object instance;
	
        _serviceBindings.TryGetValue(t, out instance);

        return instance;
    }
    
    public bool TryGetComponent(Type t, out Component component)
    {
        if (t.IsSubclassOf(typeof(Component))
        {
            return _serviceBindings.TryGetValue(t, out component);
        }
        component = null;
        return false;
    }

    /// <summary>
    /// Adds a service binding of the given type.
    /// </summary>
    /// <returns>The instance that was created.</returns>
    public T Add<T>()
    {
        return (T)Add(typeof(T), typeof(T));
    }

    /// <summary>
    /// Adds a service binding with a lookup type of T which resolves to the concrete instance U.
    /// </summary>
    /// <returns>The instance that was created.</returns>
    public T Add<T, U>() where U : T
    {
        return (T)Add(typeof(T), typeof(U));
    }

    public object Add(Type t, Type u)
    {
        return AddInternal(t, u);
    }

    /// <summary>
    /// Adds a binding for an existing service of the given binding type. Any existing binding will be removed/destroyed.
    /// </summary>
    /// <returns>The instance that was created.</returns>
    public void AddExisting<T>(T existing)
    {
        Type bindingType = existing.GetType();
        AddExistingInternal(bindingType, existing);
    }

    /// <summary>
    /// Adds a binding for an existing service with a lookup type of T which resolves to the concrete 
    /// instance U. Any existing binding will be removed/destroyed.
    /// </summary>
    /// <returns>The instance that was created.</returns>
    public void AddExisting<T, U>(U existing) where U : T
    {
        Type bindingType = typeof(T);
        AddExistingInternal(bindingType, existing);
    }

    /// <summary>
    /// Unregisters the given type and destroys the concrete instance that it resolves to.
    /// Does nothing if the type isn't registered.
    /// </summary>
    public void Remove<T>()
    {
        Remove(typeof(T));
    }
    
    /// <summary>
    /// Unregisters a type but does not destroy it.
    /// Does nothing if the type isn't registered.
    /// </summary>
    public void RemoveWithoutDestroying<T>()
    {
        RemoveWithoutDestroying(typeof(T));
    }

    /// <summary>
    /// Removes a service binding and destroys the concrete instance that it resolves to.
    /// Re-enables any previous instance for this binding.
    /// </summary>
    public void Remove(Type bindingType)
    {
        Component component;
        if (TryGetComponent(out component)
        {
             Destroy(component);
        }
	
        _serviceBindings.Remove(bindingType);
    }
    
    /// <summary>
    /// Removes a service binding, does not destroy the concrete instance.
    /// </summary>
    public void RemoveWithoutDestroying(Type bindingType)
    {
        _serviceBindings.Remove(bindingType);
    }  

    /// <summary>
    /// Adds a concrete type or replaces it, and registers the service binding.
	/// If it is a MonoBehaviour it will be attached to the service locator.
    /// </summary>
    /// <returns>The object that was created.</returns>
    private object AddInternal(Type bindingType, Type concreteType)
    {
        var concreteInstance = concreteType.IsSubclassOf(typeof(Component)) 
            ? gameObject.AddComponent(concreteType) 
            : Activator.CreateInstance(concreteType);

        _serviceBindings[bindingType] = concreteInstance;
        return concreteInstance;
    }

    /// <summary>
    /// Adds a binding for an existing service.
    /// </summary>
    private void AddExistingInternal<T>(Type bindingType, T existing)
    {
	// Destroy any existing instance of this binding.
        Component component;
        if (TryGetComponent(out component)
        {
             Destroy(component);
        }

        _serviceBindings[bindingType] = existing;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Service Locator");

        foreach (var binding in _serviceBindings) 
        {
            sb.Append("  ").Append(binding.Key).Append(" -> ").AppendLine(binding.Value.ToString());
        }

        return sb.ToString();
    }
}
