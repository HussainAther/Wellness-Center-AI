using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartTimer(float duration, System.Action onTimerComplete)
    {
        StartCoroutine(TimerCoroutine(duration, onTimerComplete));
    }

    private IEnumerator TimerCoroutine(float duration, System.Action onTimerComplete)
    {
        yield return new WaitForSeconds(duration);
        onTimerComplete?.Invoke();
    }
}

