using UnityEngine;
using System.Collections.Generic;

public interface ITimeObserver
{
    void OnTimeChanged(int hour, int minute);
}

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }
    public float TimeInterval = 2f; // Interval between time updates in seconds
    [SerializeField]private int hour;
    [SerializeField]private int minute;
    [SerializeField]private int totalTime;
    [SerializeField]public readonly int timeIncrement = 3;

    private List<ITimeObserver> observers = new List<ITimeObserver>();
    private float elapsedTime = 0f; // Keeps track of time


    public int Hour => hour; // Read-only property for hour
    public int Minute => minute; // Read-only property for minute
    public int TotalTime => totalTime; // Read-only property for total time

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        // Increment elapsed time by the time passed since the last frame
        elapsedTime += Time.deltaTime;

        // Check if the defined time interval has passed
        if (elapsedTime >= TimeInterval) // TimeInterval could be 2 seconds, for example
        {
            AddTime(0, timeIncrement); // Add timeIncrement minutes every TimeInterval
            elapsedTime = 0f; // Reset the elapsed time counter
        }
    }

    public void AddTime(int hour, int minute)
    {
        this.hour += hour;
        this.minute += minute;

        // Handle minute overflow (if minutes reach 60)
        if (this.minute >= 60)
        {
            this.minute -= 60;
            this.hour++;
        }

        // Handle hour overflow (if hours reach 24)
        if (this.hour >= 24)
        {
            this.hour -= 24; // Wrap around for 24-hour format
        }

        // Update the total time (in minutes)
        totalTime = this.hour * 60 + this.minute; 

        // Notify all observers of the time change
        NotifyObservers();
    }

    public void RegisterObserver(ITimeObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(ITimeObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnTimeChanged(hour, minute);
        }
    }
}
