using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float maxDistance = 5f;
    [SerializeField]
    private float maxImpulse = 10f;

    public enum ShotState { Nothing, Start, Charge, Release }

    public ShotState currentShotState = ShotState.Nothing;

    private PlayerInput playerInput = null;
    private new Rigidbody rigidbody = null;
    private Camera mainCamera = null;

    private Vector3 playerToMouse = Vector3.zero;

    public bool shotReady = false;
    public bool controllable = false;

    bool canGetInput = false;
    bool holding = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Start()
    {
        playerInput.OnClickPress.AddListener(CheckPlayerInputState);
        playerInput.OnClickRelease.AddListener(CheckPlayerInputState);
    }

    private void Update()
    {
        CalculatePlayerToMouse();

        CheckPlayerInputState();

        CalculatePlayerVelocity();

        CheckControllable();
    }

    public void CheckControllable()
    {
        if (controllable && shotReady)
        {
            canGetInput = true;
        }
        else
        {
            canGetInput = false;
        }
    }

    public void CalculatePlayerVelocity()
    {
        if (rigidbody.velocity.magnitude <= 0.01)
        {
            rigidbody.velocity = Vector3.zero;
            shotReady = true;
        }
        else
        {
            shotReady = false;
        }
    }

    public void CheckPlayerInputState()
    {
        if (playerInput.pressed)
        {
            ShotStart();
        }
        else if (playerInput.holded)
        {
            ShotHold();
        }
        else if (playerInput.released)
        {
            ShotReleased();
        }
        else
        {
            currentShotState = ShotState.Nothing;
        }
    }

    private void CalculatePlayerToMouse()
    {
        playerToMouse = Vector3.zero;

        Plane plane = new Plane(Vector3.up, transform.position);

        Vector3 mouseScreenPosition = playerInput.mouseScreenPosition;

        Ray mouseRay = mainCamera.ScreenPointToRay(mouseScreenPosition);

        if (plane.Raycast(mouseRay, out float distanceFromOrigin))
        {
            Vector3 hitPoint = mouseRay.GetPoint(distanceFromOrigin);
            playerToMouse = hitPoint - transform.position;
        }

        playerToMouse = new Vector3(playerToMouse.x, 0, playerToMouse.z);
    }

    private void ShotStart()
    {
        if (canGetInput)
        {
            currentShotState = ShotState.Start;
            holding = true;
        }
    }

    private void ShotHold()
    {
        currentShotState = ShotState.Charge;
    }

    private void ShotReleased()
    {
        if (holding)
        {
            float impulseMultiplier = CalculateImpulseMultiplier();

            float impulse = impulseMultiplier * maxImpulse;

            rigidbody.AddForce(playerToMouse.normalized * impulse, ForceMode.Impulse);

            currentShotState = ShotState.Release;
            holding = false;
        }
    }

    public float CalculateImpulseMultiplier()
    {
        float distancePlayerToMouse = playerToMouse.magnitude;

        if(distancePlayerToMouse > maxDistance)
        {
            distancePlayerToMouse = maxDistance;
        }

        return distancePlayerToMouse / maxDistance;
    }

    public Vector3 GetImpulseVector()
    {
        float distancePlayerToMouse = playerToMouse.magnitude;

        if (distancePlayerToMouse > maxDistance)
        {
            return playerToMouse.normalized * maxDistance;
        }

        return playerToMouse;
    }
}
