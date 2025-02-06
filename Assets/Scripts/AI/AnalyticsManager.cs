using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour
{
    // Singleton pattern for global access
    public static AnalyticsManager Instance { get; private set; }

    // Event types for tracking
    public enum AnalyticsEventType
    {
        Movement,
        Interaction,
        IdleTime,
        CustomEvent
    }

    // Data structure to store events
    private List<AnalyticsEvent> eventLog = new List<AnalyticsEvent>();

    [Header("Server Configuration")]
    [Tooltip("Endpoint URL for sending analytics data.")]
    public string serverEndpoint = "https://yourserver.com/analytics";

    private void Awake()
    {
        // Enforce singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Log an analytics event.
    /// </summary>
    /// <param name="eventType">Type of the event.</param>
    /// <param name="details">Additional details about the event.</param>
    public void LogEvent(AnalyticsEventType eventType, string details)
    {
        AnalyticsEvent newEvent = new AnalyticsEvent
        {
            eventType = eventType,
            timestamp = System.DateTime.Now.ToString(),
            details = details
        };

        eventLog.Add(newEvent);

        Debug.Log($"[Analytics] Logged Event: {newEvent.eventType} at {newEvent.timestamp}");
    }

    /// <summary>
    /// Send logged data to the server.
    /// </summary>
    public void SendDataToServer()
    {
        if (eventLog.Count == 0)
        {
            Debug.LogWarning("[Analytics] No events to send.");
            return;
        }

        StartCoroutine(SendDataCoroutine());
    }

    private System.Collections.IEnumerator SendDataCoroutine()
    {
        List<AnalyticsEvent> dataToSend = new List<AnalyticsEvent>(eventLog);
        string jsonData = JsonUtility.ToJson(new AnalyticsDataWrapper { events = dataToSend });

        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Post(serverEndpoint, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.Log("[Analytics] Data sent successfully.");
                eventLog.Clear();
            }
            else
            {
                Debug.LogError($"[Analytics] Failed to send data: {www.error}");
            }
        }
    }

    /// <summary>
    /// Print all events in the log to the console.
    /// </summary>
    public void DebugPrintLog()
    {
        Debug.Log("[Analytics] Current Event Log:");
        foreach (var e in eventLog)
        {
            Debug.Log($"Type: {e.eventType}, Timestamp: {e.timestamp}, Details: {e.details}");
        }
    }

    // Event data structure
    [System.Serializable]
    public class AnalyticsEvent
    {
        public AnalyticsEventType eventType;
        public string timestamp;
        public string details;
    }

    // Wrapper for JSON serialization
    [System.Serializable]
    public class AnalyticsDataWrapper
    {
        public List<AnalyticsEvent> events;
    }
}

