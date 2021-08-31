using UnityEngine;
using UnityEngine.InputSystem;

public class GameControls : MonoBehaviour, Controls.IDefaultActions
{
    [SerializeField]
    private float _zoomSensitively = 0.1f;
    [SerializeField, Range(0f, 50f)]
    private float _maxZoom = 5f;
    [SerializeField, Range(0f, 50f)]
    private float _minZoom = 1f;

    private Controls _controls;
    private Vector2 _previousPosition;
    private bool _isPressed;

    void Controls.IDefaultActions.OnPosition(InputAction.CallbackContext context)
    {
        var position = context.ReadValue<Vector2>();
        if (_isPressed)
        {
            var delta = position - _previousPosition;
            transform.Translate(- OrthographicPixelsToWorld(delta));
        }
        _previousPosition = position;
    }

    void Controls.IDefaultActions.OnPress(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                _isPressed = true;
                break;
            case InputActionPhase.Canceled:
                _isPressed = false;
                break;
        }
    }

    void Controls.IDefaultActions.OnZoom(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        if(value.y >= 1f)
        {
            OrthographicChangeZoom(- _zoomSensitively);
        }
        if(value.y <= -1f)
        {
            OrthographicChangeZoom(_zoomSensitively);
        }
    }

    public void EnableControls() => _controls.Default.Enable();
    public void DisableControls() => _controls.Default.Disable();

    private void OrthographicChangeZoom(float delta)
    {
        Camera.main.orthographicSize += delta;
        Mathf.Clamp(Camera.main.orthographicSize, _minZoom, _maxZoom);
    }

    private Vector2 OrthographicPixelsToWorld(Vector2 pixels)
    {
        var cam = Camera.main;
        var cameraDiagonal = new Vector2(2 * cam.orthographicSize * Screen.width / Screen.height, 2 * cam.orthographicSize);
        return new Vector2(pixels.x * cameraDiagonal.x / Screen.width, pixels.y * cameraDiagonal.y / Screen.height);
    }

    private void OnEnable()
    {
        _controls = new Controls();
        _controls.Enable();
        _controls.Default.SetCallbacks(this);
    }

    private void OnDisable()
    {
        _controls.Disable();
        _controls.Dispose();
    }
    
}

