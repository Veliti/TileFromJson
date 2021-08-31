using UnityEngine;
using UnityEngine.Events;

public class MenuUI : MonoBehaviour
{
    public UnityEvent OnMenuEnabled;
    public UnityEvent OnMenuDisabled;

    public void EnableMenu()
    {
        gameObject.SetActive(true);
        OnMenuEnabled?.Invoke();
    }
    public void DisableMenu()
    {
        gameObject.SetActive(false);
        OnMenuDisabled?.Invoke();
    }

}
