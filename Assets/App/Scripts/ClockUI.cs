using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI amPmText;

    private CancellationTokenSource _cts;
    private bool _initialized;

    private void Awake()
    {
        _cts = new CancellationTokenSource();
    }

    private async void Start()
    {
        UpdateClockText();
        
        // Wait until the next minute starts
        var dateTimeNow = DateTime.Now;
        var initialWaitUntilNextMin = Mathf.Abs(dateTimeNow.Second - 60);
        await UniTask.Delay(initialWaitUntilNextMin * 1000);
        
        _initialized = true;
    }

    private void OnDestroy()
    {
        _cts.Cancel();
        _cts.Dispose();
        _cts = null;
    }

    private async void FixedUpdate()
    {
        if (!_initialized) return;

        while (!_cts.IsCancellationRequested)
        {
            UpdateClockText();

            // Wait for the next minute to update the text
            await UniTask.Delay(60000);
        }
    }

    private void UpdateClockText()
    {
        var dateTimeNow = DateTime.Now;
        timeText.text = dateTimeNow.ToString("h:mm");
        amPmText.text = dateTimeNow.Hour > 11 ? "PM" : "AM";
    }
}
