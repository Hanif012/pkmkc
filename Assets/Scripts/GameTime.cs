using UnityEngine;
using System.Collections.Generic;

public interface ITimeObserver
{
    void OnTimeChanged(int hour, int minute);
}

public class GameTime : MonoBehaviour
{
    public static GameTime Instance { get; private set; }
    public float TimeInterval = 2f;

    public int hour;
    public int minute;
    public int TotalTime;

    private List<ITimeObserver> observers = new List<ITimeObserver>();
    private float elapsedTime = 0f; // Keeps track of time

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

        // Check if 1 second has passed
        if (elapsedTime >= TimeInterval) // 1 second has passed
        {
            AddTime(0, 3); // Add 3 minutes
            elapsedTime = 0f; // Reset the elapsed time counter
        }
        // Debug.Log(observers.Count); // Cek jumlah observer
    }

    public void AddTime(int hour, int minute)
    {
        this.hour += hour;
        this.minute += minute;

        if (this.minute >= 60)
        {
            this.minute -= 60;
            this.hour++;
        }

        if (this.hour >= 24)
        {
            this.hour -= 24; // Wrap around for 24-hour format
        }

        TotalTime = this.hour * 60 + this.minute; // Update TotalTime
        NotifyObservers(); // Notify all observers of the time change
    }

    public int GetTime()
    {
        return hour * 60 + minute;
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
