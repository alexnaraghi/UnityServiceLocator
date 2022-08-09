using UnityEngine;

/// <summary>
/// Utility class that auto-adds and destroys mono-behaviour based services with the same scope as whatever it's attached to.
/// You can inherit from this installer to add non-MonoBehaviour objects or add an interface.
/// </summary>
public class ServiceInstaller : MonoBehaviour
{
    [SerializeField] protected Object[] _components;
    protected ServiceLocator _locator;

    protected virtual void Awake()
    {
        _locator = Services.instance;

        foreach (var component in _components)
        {
            if (component != this && component != null)
                _locator.AddExisting(component);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_components == null || _locator == null) 
            return;
        
        foreach (var component in _components)
        {
            if (component != this && component != null)
                _locator.Remove(component.GetType());
        }
    }
}