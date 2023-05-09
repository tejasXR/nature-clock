using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite isOnSprite;
    [SerializeField] private Sprite isOffSprite;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(ChangeIconSprite);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(ChangeIconSprite);
    }

    private void Start()
    {
        toggle.isOn = true;
    }

    private void ChangeIconSprite(bool isToggleOn)
    {
        icon.sprite = isToggleOn ? isOnSprite : isOffSprite;
        AudioListener.volume = isToggleOn ? 1 : 0;
    }
}
