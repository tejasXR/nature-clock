using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

[RequireComponent(typeof(Button))]
public class FocusButton : MonoBehaviour, IPointerClickHandler
{
    public event Action Clicked;

    [SerializeField] private TextMeshProUGUI focusButtonText;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Only register left clicks
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        
        Clicked?.Invoke();
    }

    public void ChangeButtonText(string buttonText)
    {
        focusButtonText.text = buttonText;
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
}
