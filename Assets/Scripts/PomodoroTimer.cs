using System;
using TMPro;
using UnityEngine;

public class PomodoroTimer : MonoBehaviour
{
    [SerializeField] private int workTime = 5;
    [SerializeField] private int breakTime = 1;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool _countdown;
    private TimeSpan _timeLeft;

    private void Awake()
    {
        StartTimer(TimeSpan.FromSeconds(workTime));
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

        timerText.text = _timeLeft.ToString("g");
    }

    private void SetTimeToCountDown(TimeSpan timeSpan)
    {
        _timeLeft = timeSpan;
    }
}
