using UnityEngine;

public static class Logger
{
    private static bool isLoggingEnabled = true;

    public static void Log(string message)
    {
        if (isLoggingEnabled)
        {
            Debug.Log($"[LOG]: {message}");
        }
    }

    public static void LogWarning(string message)
    {
        if (isLoggingEnabled)
        {
            Debug.LogWarning($"[WARNING]: {message}");
        }
    }

    public static void LogError(string message)
    {
        if (isLoggingEnabled)
        {
            Debug.LogError($"[ERROR]: {message}");
        }
    }

    public static void SetLogging(bool enable)
    {
        isLoggingEnabled = enable;
    }
}

