# Unity Service Locator
General purpose, fast, concise implementation of a service locator for Unity, supporting both MonoBehaviours and pure C# classes.

A typical Unity game needs global access to some objects with full control over instantiation time (NO singletons, NO lazy instantiation),
and readable, lightweight code without "enterprise" bloat (NO dependency injection). We're talking under 200 lines of code.

In addition, this package supports unit testing, interfaces, and scoped containers.

I have been using this pattern in Unity with minimal changes for more than 6 years, in multiple small to medium-sized codebases (400k+ lines, 1-10 developers). It's stupid simple to understand and works great for teams.

1. [Get Started](#get-started)
2. [MonoBehaviours](#MonoBehaviours)
3. [Pure C#](#Pure_C#)
4. [Installer](Installer)
5. [Examples](Examples)
6. [Pull Requests](Pull-Requests)

---

## Get Started
Provided are the 3 core classes (<i>Services, ServiceLocator, and ServiceInstaller</i>), and some examples. The most common usage is global access through <i>Services</i>.
#### Add a service
```C#
Services.Add<YourMonoBehaviour>();
Services.Add<YourPureC#Class>();
Services.AddExisting(someObject);
```

#### Add a service by interface
```C#
Services.Add<IYourInterface, YourConcreteType>();
```

#### Access a service
```C#
Services.Get<YourMonoBehaviour>();
Services.Get<IYourInterface>();
```

#### Remove a service
```C#
Services.Remove<YourMonoBehaviour>();
```

See the included examples for common access patterns.

## MonoBehaviours
The default service locator creates a GameObject with DontDestroyOnLoad and any new MonoBehaviour service will be added to that game object. Your services will exist across scenes unless you choose to remove them.

For scene-specific services, the <i>ServiceInstaller</i> will automatically add and remove MonoBehaviours set via inspector.

## Pure C#
The Service Locator can bind any type. Pure C# objects simply won't show up in the inspector, but in many cases they are preferable. You access pure C# objects in exactly the same way as MonoBehaviours, with 'Services.Get()'.

## Installer
Every game I've ever worked on has services that depend on each other and need to be instantiated in a well-defined order. That is why I recommend creating and adding services in an installer/bootstrap class at startup and/or in scenes. 

The <i>ServiceInstaller</i> will do exactly this for MonoBehaviours, but you can inherit from it or write your own for complete control over how things are instantiated.

Avoid lazy instantiation, it doesn't work for games because it creates lag spikes and dependency bugs. Create and register services in as few places as possible to make dependencies clear.
## Flexibility
A service locator is NOT a game engine pattern, it does not define how you make a game and you can use it for as much or as little as you want. 

You can add a <i>ServiceLocator</i> into a mature project and simply use it to globally access 3rd party APIs, or you could use it to access game APIs like a camera system. Add interfaces for unit tests or cross-platform code.

For example, the static <i>Services</i> class is provided for global access, but if you wanted multiple service locators or another pattern, simply create a service locator yourself and provide your own form of access.
```C#
var serviceLocator = new GameObject().AddComponent<ServiceLocator>();
```

At the end of the day, it's just a Dictionary of (Type -> object) with some utility methods.

## Examples
Included is an Examples folder that illustrates how to add and access services.

<i>ExampleServiceInstaller</i> shows how to add new services in several different ways. <i>GameUI</i> shows how to access services from anywhere.

These examples don't demonstrate game principles, just how to use a service locator.

## Pull Requests
Feel free to contribute or provide feedback if you feel anything is missing.