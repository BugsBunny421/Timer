# Timer
Yet another Powerful and convenient library for running actions after a delay in Unity Engine.
It is a lightweight utility library for managing timers and timed callbacks in Unity projects.

## Features

- Create timers with specified durations and actions to be executed upon completion.
- Add timed callbacks that trigger at specific intervals during a timer's execution.
- Set an owner for a timer to automatically stop and remove it when the owner is destroyed.
- Easily manage multiple timers using the TimersManager utility.

## Installation

Rightnow, There's no package that you can install. Just Download the repository and put the contents in your Unity Project.

## Usage

### Example 1: Basic Timer

```csharp
// Create a timer that fires after 5 seconds
Timer timer = new Timer(5f, () =>
{
    Debug.Log("Timer finished!");
});

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);
```

### Example 2: Adding a method on each update of Timer

```csharp
// Create a timer that fires after 10 seconds and displays the remaining time in each update
Timer timer = new Timer(10f, () =>
{
    Debug.Log("Timer finished!");
});

timer.AddOnUpdateAction((remainingTime) =>
{
    Debug.Log("Remaining time: " + remainingTime);
});

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);
```

### Example 3: Adding a Timed Callback

```csharp
// Create a timer that fires after 5 seconds
Timer timer = new Timer(5f, () =>
{
    Debug.Log("Timer finished!");
});

// Add a timed callback that fires after 2 seconds
timer.AddCallback(2f, () =>
{
    Debug.Log("Timed callback fired!");
});

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);
```

### Example 4: Stopping a Timer

```csharp
// Create a timer that fires after 5 seconds
Timer timer = new Timer(5f, () =>
{
    Debug.Log("Timer finished!");
});

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);

// Stop the timer after 2 seconds
timer.Stop();
Debug.Log("Timer stopped!");
```

### Example 5: Checking Timer Status

```csharp
// Create a timer that fires after 5 seconds
Timer timer = new Timer(5f, () =>
{
    Debug.Log("Timer finished!");
});

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);

// Check the status of the timer in the Update method
void Update()
{
    if (timer.IsFinished())
    {
        Debug.Log("Timer has finished!");
    }
}
```

### Example 6: Setting Timer Owner

```csharp
// Create a timer that fires after 5 seconds
Timer timer = new Timer(5f, () =>
{
    Debug.Log("Timer finished!");
});

// Set the owner of the timer to a GameObject
timer.SetOwner(gameObject);

// Add the timer to the TimersManager for automatic execution
TimersManager.Add(timer);
```

In this example, the timer's owner is set to a GameObject using the `SetOwner()` method. When the owner is destroyed, the timer is automatically stopped and removed from the TimersManager.

## Contributing

Contributions to this Timer are welcome! If you find any issues or have suggestions for improvements, please create an issue or submit a pull request.

## License

This Timer is released under the MIT License. See the [LICENSE](https://github.com/BugsBunny421/Timer/blob/main/LICENSE) file for more details.

---
