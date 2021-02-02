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

    public void ShotButtonEvent(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            pressed = true;
            holded = true;
            released = false;
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            pressed = false;
            released = true;
            holded = false;
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
