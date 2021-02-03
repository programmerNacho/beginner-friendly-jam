using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    PlayerMovement playerMovement;
    Camera mainCamera = null;

    Vector3 playerToMouse = Vector3.zero;

    private void Start()
    {
        InicializeVariables();
    }

    private void InicializeVariables()
    {
        playerMovement = GetComponent<PlayerMovement>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (playerMovement.GetShotHolding())
        {
            CalculatePlayerToMouse();
            playerMovement.SetPlayerToMove(playerToMouse);
        }
    }
    public void ShotButtonEvent(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerMovement.ShotDown();
        }
        else if (context.canceled)
        {
            CalculatePlayerToMouse();
            playerMovement.ShotUp(playerToMouse);
        }
    }
    private void CalculatePlayerToMouse()
    {
        playerToMouse = Vector3.zero;

        Plane plane = new Plane(Vector3.up, transform.position);

        Vector3 mouseScreenPosition = Mouse.current.position.ReadValue();

        Ray mouseRay = mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (plane.Raycast(mouseRay, out float distanceFromOrigin))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distanceFromOrigin);
            playerToMouse = hitPoint - transform.position;
        }
    }

    //public bool pressed = false;
    //public bool holded = false;
    //public bool released = false;

    //public Vector2 mouseScreenPosition = Vector2.zero;

    //public UnityEvent OnClickPress = new UnityEvent();
    //public UnityEvent OnClickRelease = new UnityEvent();

    //private void Start()
    //{
    //    pressed = false;
    //    holded = false;
    //    released = false;
    //}

    //public void ShotButtonEvent(InputAction.CallbackContext context)
    //{
    //    if(context.phase == InputActionPhase.Started)
    //    {
    //        pressed = true;
    //        holded = true;
    //        released = false;
    //        OnClickPress.Invoke();
    //    }
    //    else if(context.phase == InputActionPhase.Canceled)
    //    {
    //        pressed = false;
    //        released = true;
    //        holded = false;
    //        OnClickRelease.Invoke();
    //    }
    //}

    //private void Update()
    //{
    //    mouseScreenPosition = Mouse.current.position.ReadValue();

    //    if(holded)
    //    {
    //        pressed = false;
    //    }
    //    else if(released)
    //    {
    //        released = false;
    //    }
    //}


}
