using System;
using UnityEngine;

public class PomodoroTimer : MonoBehaviour
{
    private bool _countdown;
    private TimeSpan _timeLeft;
    
    public PomodoroTimer(TimeSpan timer)
    {
        
    }

    private void StartTimer(TimeSpan timeSpan)
    {
        SetTimeToCountDown(timeSpan);
        _countdown = true;
    }

    private void Update()
    {
        if (_countdown)
            Countdown();    
    }

    private void Countdown()
    {
        TimeSpan deltaTimeSpan = TimeSpan.FromSeconds(Time.deltaTime);
        _timeLeft = _timeLeft.Subtract(deltaTimeSpan);
    }

    private void SetTimeToCountDown(TimeSpan timeSpan)
    {
        _timeLeft = timeSpan;
    }
}
