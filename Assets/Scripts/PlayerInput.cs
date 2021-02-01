using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerInput : MonoBehaviour
{
    //Va a guardar los input
    #region Variables
    PlayerMovement playerMovement;

    public bool inverse = false; // Invertir la direccion del golpe

    public float mouseInUseTime = 0.1f; // Cuanto tiempo debe estar el raton parado para ignorarlo.
    public float forceDistance = 0.1f;

    public LayerMask rayLayer;

    float mouseInUse = 0;
    bool active = true;

    Vector3 impulseDirection = Vector3.zero;
    float impulseForce = 0;
    #endregion

    #region Inicialicar
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.BallStopped.AddListener(Active);
        playerMovement.OnBallShot.AddListener(Desactive);
    }
    #endregion

    #region Inputs
    public void Active()
    {
        active = true;
    }
    public void Desactive()
    {
        active = false;
    }

    public void SetMouseDelta(InputAction.CallbackContext context)
    {
        mouseInUse = mouseInUseTime;
    }
    #endregion

    #region Procesos
    private void Update()
    {
        if (active)
        {
            if (mouseInUse > 0) mouseInUse -= Time.deltaTime;
            CalculateAddres();
        }
    }

    void CalculateAddres()
    {
        if (mouseInUse > 0)
        {
            impulseDirection = MouseDirection();
            impulseForce = MouseImpulse();
        }

        SetLine();
    }

    Vector3 MouseDirection()
    {
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Physics.Raycast(ray, out hit, 100, rayLayer, QueryTriggerInteraction.Collide);

        Vector3 direction;

        if (inverse) direction = (transform.position - hit.point).normalized;
        else direction = (hit.point - transform.position).normalized;

        return new Vector3(direction.x, 0, direction.z);
    }

    float MouseImpulse()
    {
        Vector2 ballDir = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mouseDir = Mouse.current.position.ReadValue();

        float force = Vector2.Distance(ballDir, mouseDir) * forceDistance;
        force = Mathf.Clamp(force, 0, playerMovement.maxForce);

        return force;
    }

    void SetLine()
    {
        Vector3 posA, posB;

        if (!inverse)
        {
            posA = transform.position;
            posB = transform.position + (impulseDirection * impulseForce);
        }
        else
        {
            posA = transform.position + (impulseDirection * impulseForce);
            posB = transform.position;
        }

        playerMovement.DrawLine(posA, posB);
    }
    #endregion

    #region Outputs

    public void Shot(InputAction.CallbackContext context)
    {
        if (active)
        {
            playerMovement.ShotBall(impulseDirection, impulseForce);
        }
    }
    #endregion
}
