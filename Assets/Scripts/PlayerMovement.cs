using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigi = null;

    [SerializeField]
        private float maxDistance = 5f;
    [SerializeField]
        private float maxImpulse = 10f;
    [SerializeField]
        private float velocityToStoping = 0.2f;
    [SerializeField]
        private float airResistance = 0.1f;

    public UnityEvent OnSpawnStart = new UnityEvent(); // Comienza a aparecer
    public UnityEvent OnSpawnEnd = new UnityEvent(); // Termina de aparecer
    public UnityEvent OnDisappearStart = new UnityEvent(); // Comienza a desaparecer
    public UnityEvent OnDisappearEnd = new UnityEvent(); // Termina de desaparecer

    public UnityEvent OnShotStart = new UnityEvent(); // Comienza a cargar el disparo
    public UnityEvent OnShotEnd = new UnityEvent(); // Deja d cargar el disparo
    public UnityEvent OnBallMove = new UnityEvent(); // La bola comienza a moverse
    public UnityEvent OnBallStopped = new UnityEvent(); // La bola se detiene completamente
    public UnityEvent OnBallCollided = new UnityEvent(); // Termina de desaparecer


    private bool shotHolding = false;
    private bool controllable = true;
    private bool shotReady = false;

    private bool canGetInput = false;

    [SerializeField] private LayerMask GroundLayer;
    Vector3 groundPosition = Vector3.zero;
    [SerializeField] private float groundRadio = 0.11f;
    bool isGrounded = false;
    [SerializeField] private bool canShotInAir = true;

    private Vector3 playerToMouse = Vector3.zero;

    private void Start()
    {
        InicializeVariables();
        SubcribeToEvents();
    }

    private void InicializeVariables()
    {
        rigi = GetComponent<Rigidbody>();
    }

    private void SubcribeToEvents()
    {
        OnSpawnEnd.AddListener(SpawnCompleted);
    }

    private void Update()
    {
        CheckControllable();
        ShotPressed();
        CheckGround();
    }

    private void CheckGround()
    {
        if (canShotInAir) isGrounded = true;
        else
        {
            groundPosition = transform.position + (Vector3.down * 0.01f);
            isGrounded = Physics.CheckSphere(groundPosition, groundRadio, GroundLayer);
        }
    }
    public void ShotDown()
    {
        if (canGetInput)
        {
            shotHolding = true;
            OnShotStart.Invoke();
        }
    }

    private void ShotPressed()
    {
        if (!canGetInput) OnShotEnd.Invoke();
    }

    public void ShotCanceled()
    {
        OnShotEnd.Invoke();
    }
    public void ShotUp(Vector3 shotVector)
    {
        if (canGetInput)
        {
            if (shotHolding)
            {

                shotHolding = false;
                playerToMouse = shotVector;

                GetImpulseVector();

                ShotBall();
                OnShotEnd.Invoke();
            }
        }
    }
    private void ShotBall()
    {
        float impulseMultiplier = CalculateImpulseMultiplier();

        float impulse = impulseMultiplier * maxImpulse;

        rigi.AddForce(playerToMouse.normalized * impulse, ForceMode.Impulse);

        OnBallMove.Invoke();
    }
    public float CalculateImpulseMultiplier()
    {
        float distancePlayerToMouse = playerToMouse.magnitude;

        if (distancePlayerToMouse > maxDistance)
        {
            distancePlayerToMouse = maxDistance;
        }

        return distancePlayerToMouse / maxDistance;
    }
    public void CheckControllable()
    {
        CalculatePlayerVelocity();
        //if (controllable && shotReady && isGrounded)
        if (controllable && isGrounded)
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
        if (rigi.velocity.magnitude <= 0.02)
        {
            rigi.velocity = Vector3.zero;

            if (shotReady == false)
            {
                OnBallStopped.Invoke();
                shotReady = true;
            }
        }
        else
        {
            if (rigi.velocity.magnitude <= velocityToStoping)
            {
                rigi.velocity = Vector3.MoveTowards(rigi.velocity, Vector3.zero, airResistance * Time.deltaTime);
            }
            shotReady = false;
        }
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

    public void Spawn()
    {
        OnSpawnStart.Invoke();
    }

    public void SpawnCompleted()
    {
        controllable = true;
    }
    public void Disappear()
    {
        controllable = false;
        OnDisappearStart.Invoke();
    }

    public void SetPlayerToMove(Vector3 value)
    {
        playerToMouse = value;
    }
    public Vector3 GetPlayerToMouse()
    {
        return playerToMouse;
    }
    public void SetControllable(bool value)
    {
        controllable = value;
    }
    public bool GetControllable()
    {
        return controllable;
    }
    public bool GetShotHolding()
    {
        return shotHolding;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > 1.5f)
        {
            OnBallCollided.Invoke();
        }
    }

    //[SerializeField]
    //private float maxDistance = 5f;
    //[SerializeField]
    //private float maxImpulse = 10f;

    //public enum ShotState { Nothing, Start, Charge, Release }

    //public ShotState currentShotState = ShotState.Nothing;

    //private PlayerInput playerInput = null;
    //private new Rigidbody rigidbody = null;
    //private Camera mainCamera = null;

    //private Vector3 playerToMouse = Vector3.zero;

    //public bool shotReady = false;
    //public bool controllable = false;

    //bool canGetInput = false;
    //bool holding = false;

    //private void Awake()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //    rigidbody = GetComponent<Rigidbody>();
    //    mainCamera = Camera.main;
    //}

    //private void Start()
    //{
    //    playerInput.OnClickPress.AddListener(CheckPlayerInputState);
    //    playerInput.OnClickRelease.AddListener(CheckPlayerInputState);
    //}

    //private void Update()
    //{
    //    CalculatePlayerToMouse();

    //    CheckPlayerInputState();

    //    CalculatePlayerVelocity();

    //    CheckControllable();
    //}


    //public void CheckPlayerInputState()
    //{
    //    if (playerInput.pressed)
    //    {
    //        ShotStart();
    //    }
    //    else if (playerInput.holded)
    //    {
    //        ShotHold();
    //    }
    //    else if (playerInput.released)
    //    {
    //        ShotReleased();
    //    }
    //    else
    //    {
    //        currentShotState = ShotState.Nothing;
    //    }
    //}

    //private void CalculatePlayerToMouse()
    //{
    //    playerToMouse = Vector3.zero;

    //    Plane plane = new Plane(Vector3.up, transform.position);

    //    Vector3 mouseScreenPosition = playerInput.mouseScreenPosition;

    //    Ray mouseRay = mainCamera.ScreenPointToRay(mouseScreenPosition);

    //    if (plane.Raycast(mouseRay, out float distanceFromOrigin))
    //    {
    //        Vector3 hitPoint = mouseRay.GetPoint(distanceFromOrigin);
    //        playerToMouse = hitPoint - transform.position;
    //    }

    //    playerToMouse = new Vector3(playerToMouse.x, 0, playerToMouse.z);
    //}

    //private void ShotStart()
    //{
    //    if (canGetInput)
    //    {
    //        currentShotState = ShotState.Start;
    //        holding = true;
    //    }
    //}

    //private void ShotHold()
    //{
    //    currentShotState = ShotState.Charge;
    //}

    //private void ShotReleased()
    //{
    //    if (holding)
    //    {
    //        float impulseMultiplier = CalculateImpulseMultiplier();

    //        float impulse = impulseMultiplier * maxImpulse;

    //        rigidbody.AddForce(playerToMouse.normalized * impulse, ForceMode.Impulse);

    //        currentShotState = ShotState.Release;
    //        holding = false;
    //    }
    //}

    //public float CalculateImpulseMultiplier()
    //{
    //    float distancePlayerToMouse = playerToMouse.magnitude;

    //    if(distancePlayerToMouse > maxDistance)
    //    {
    //        distancePlayerToMouse = maxDistance;
    //    }

    //    return distancePlayerToMouse / maxDistance;
    //}

    //public Vector3 GetImpulseVector()
    //{
    //    float distancePlayerToMouse = playerToMouse.magnitude;

    //    if (distancePlayerToMouse > maxDistance)
    //    {
    //        return playerToMouse.normalized * maxDistance;
    //    }

    //    return playerToMouse;
    //}
}
