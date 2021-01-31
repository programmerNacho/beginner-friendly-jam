using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    // Estado de la bola
    // Va a leer los inputs y a mover la bola

    #region Variables
    Rigidbody rigi;
    LineRenderer line;

    public float forceMult = 1.5f;
    public float maxForce = 5;

    public UnityEvent BallStopped;
    public UnityEvent OnBallShot;

    bool stoped = true;

    #endregion

    #region Inicialicar
    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
    }
    #endregion

    #region Inputs
    #endregion

    #region Procesos
    private void Update()
    {
        if (rigi.velocity.magnitude <= 0.3f && !stoped)
        {
            rigi.velocity = Vector3.zero;
            BallStopped.Invoke();
            line.enabled = true;
            stoped = true;
        }
    }
    public void ShotBall(Vector3 direction, float force)
    {
        rigi.AddForce(direction * force * forceMult, ForceMode.Impulse);

        OnBallShot.Invoke();
        line.enabled = false;

        stoped = false;
    }

    public void DrawLine(Vector3 posA, Vector3 posB)
    {
        line.SetPosition(0, posA);
        line.SetPosition(1, posB);
    }
    #endregion

    #region Outputs
    #endregion
}
