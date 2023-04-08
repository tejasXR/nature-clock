using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FocusController : MonoBehaviour
{
    [SerializeField] private FocusButton focusButton;
    [SerializeField] private Button stopFocusButton;

    [Space]
    [SerializeField] private TextMeshProUGUI debugTimeWorked;
    [SerializeField] private TextMeshProUGUI debugBreakLeft;

    private const string StartFocusing = "Start Focusing";
    private const string TakeABreak = "Take a Break";
    
    private enum FocusState
    {
        Idle,
        Working,
        Break
    }

    private FocusState _focusState = FocusState.Idle;
    private TimeSpan _timeWorked;
    private TimeSpan _breakTimeLeft;

    private void Awake()
    {
        focusButton.Clicked += OnFocusButtonPressed;
        stopFocusButton.onClick.AddListener(OnStopFocusButtonPressed);
    }

    private void OnDestroy()
    {
        focusButton.Clicked -= OnFocusButtonPressed;
        stopFocusButton.onClick.RemoveListener(OnStopFocusButtonPressed);
    }
    
    private void OnFocusButtonPressed()
    {
        switch (_focusState)
        {
            case FocusState.Idle:
            case FocusState.Break:
                ChangeFocusState(FocusState.Working);
                break;
            case FocusState.Working:
                ChangeFocusState(FocusState.Break);
                break;
        }
    }

    private void OnStopFocusButtonPressed()
    {
        ChangeFocusState(FocusState.Idle);
    }

    private void ChangeFocusState(FocusState newFocusState)
    {
        switch (newFocusState)
        {
            case FocusState.Idle:
                focusButton.ChangeButtonText(StartFocusing);
                break;
            case FocusState.Working:
                StartFocus();
                break;
            case FocusState.Break:
                StartBreak();
                break;
        }

        _focusState = newFocusState;
    }
    
    private void StartFocus()
    {
        _timeWorked = TimeSpan.Zero;
        focusButton.ChangeButtonText(TakeABreak);
    }

    private void StartBreak()
    {
        focusButton.ChangeButtonText(StartFocusing);
        _breakTimeLeft = TimeSpan.FromMinutes(GetBreakTime());
    }

    private int GetBreakTime()
    {
        const int incrementOne = 10;
        const int incrementOneBreak = 2;
        const int incrementTwo = 15;
        const int incrementTwoBreak = 3;
        const int incrementThree = 30;
        const int incrementThreeBreak = 5;
        const int incrementFour = 45;
        const int incrementFourBreak = 7;
        
        switch (_timeWorked.Minutes)
        {
            case > incrementOne and < incrementTwo:
                return incrementTwoBreak;
            case > incrementTwo and < incrementThree:
                return incrementThreeBreak;
            case > incrementThree and < incrementFour:
                return incrementFourBreak;
        }
        
        return incrementOneBreak;
    }

    private void Update()
    {
        if (_focusState == FocusState.Working)
        {
            _timeWorked = _timeWorked.Add(TimeSpan.FromSeconds(Time.deltaTime));
            debugTimeWorked.text = $"Working Time: {_timeWorked:g}";
        }

        if (_focusState == FocusState.Break)
        {
            if (_breakTimeLeft.TotalSeconds <= 0) return;
            _breakTimeLeft = _breakTimeLeft.Subtract(TimeSpan.FromSeconds(Time.deltaTime));
            debugBreakLeft.text = $"Break Time: {_breakTimeLeft:g}";
        }
    }
}
