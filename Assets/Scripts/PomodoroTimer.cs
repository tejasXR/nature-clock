using System;
using UnityEngine;

public class PomodoroTimer : MonoBehaviour
{
    [SerializeField] private int workTime = 5;
    [SerializeField] private int breakTime = 1;

    private bool _countdownTime;
    private TimeSpan _timeLeft;
    
    private void StartTimer(TimeSpan timeSpan)
    {
        SetTimeToCountDown(timeSpan);
        _countdownTime = true;
    }

    private void Update()
    {
        if (_countdownTime)
            CountdownTime();    
    }

    private void CountdownTime()
    {
        
    }

    private void SetTimeToCountDown(TimeSpan timeSpan)
    {
        _timeLeft = timeSpan;
    }
}
