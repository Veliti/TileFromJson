using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MenuTextUI : MonoBehaviour
{
    public TextCallbackEvent UpdateTextEvent;
    private TMP_Text _text;

    public void UpdateText()
    {
        UpdateTextEvent?.Invoke((string text) => _text.text = text);
    }

    private void Awake() => _text = GetComponent<TMP_Text>();

    [Serializable]
    public class TextCallbackEvent : UnityEvent<Action<string>> {}
}
