using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour, ITimeObserver
{
    public TextMeshProUGUI timeText;
    private int _hour = 0;
    private int _minute = 0;

    private float colonToggleTime = 0.5f; // 0.5 second toggle interval
    private float elapsedTime = 0f;
    private bool showColon = true; // Controls whether to show the colon or not

    void Start()
    {
        if (timeText == null)
            timeText = GetComponent<TextMeshProUGUI>();

        GameTime.Instance.RegisterObserver(this);  // Register as observer
    }

    void OnDestroy()
    {
        // Ensure to unregister the observer when the object is destroyed
        if (GameTime.Instance != null)
        {
            GameTime.Instance.UnregisterObserver(this);
        }
    }

    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Toggle colon every half second
        if (elapsedTime >= colonToggleTime)
        {
            showColon = !showColon; // Toggle the colon visibility
            UpdateTimeDisplay(); // Update the time text with or without the colon
            elapsedTime = 0f; // Reset the elapsed time
        }
    }

    // This method is called whenever the time changes
    public void OnTimeChanged(int hour, int minute)
    {
        _hour = hour;
        _minute = minute;
        UpdateTimeDisplay(); // Update the time display when the time changes
    }

    // Updates the displayed time, toggling the colon
    private void UpdateTimeDisplay()
    {
        string colon = showColon ? ":" : " "; // Show colon or blank space
        timeText.text = $"{_hour:00}{colon}{_minute:00}"; // Update the time display with/without colon
    }
}
