using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInput : MonoBehaviour
{
    public bool pressed = false;
    public bool holded = false;
    public bool released = false;

    public Vector2 mouseScreenPosition = Vector2.zero;

    public UnityEvent OnClickPress = new UnityEvent();
    public UnityEvent OnClickRelease = new UnityEvent();

    private void Start()
    {
        pressed = false;
        holded = false;
        released = false;
    }

    public void ShotButtonEvent(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            pressed = true;
            holded = true;
            released = false;
            OnClickPress.Invoke();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            pressed = false;
            released = true;
            holded = false;
            OnClickRelease.Invoke();
        }
    }

    private void Update()
    {
        mouseScreenPosition = Mouse.current.position.ReadValue();
        
        if(holded)
        {
            pressed = false;
        }
        else if(released)
        {
            released = false;
        }
    }
}
