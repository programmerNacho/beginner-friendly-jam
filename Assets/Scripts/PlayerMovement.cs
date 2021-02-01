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

    Checkpoint checkpoint = null;
    bool respawn = false;
    public float checkpointSpeed = 5;
    public ParticleSystem deadParticles;

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
        BallVelocity();
        MoveYoCheckpoint();
    }
    void BallVelocity()
    {
        if (rigi.velocity.magnitude <= 0.1f && !stoped)
        {
            rigi.velocity = Vector3.zero;
            BallStopped.Invoke();
            line.enabled = true;
            stoped = true;
        }
    }
    void MoveYoCheckpoint()
    {
        if (respawn)
        {
            if (transform.position != checkpoint.SpawnPoint.transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, checkpoint.SpawnPoint.transform.position, checkpointSpeed * Time.deltaTime);
            }
            else
            {
                line.enabled = true;
                respawn = false;
                SetRigibody(true);
                BallStopped.Invoke();
            }
        }
    }

    void SetRigibody(bool value)
    {
        rigi.isKinematic = !value;
        rigi.useGravity = value;
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

    public void ReturnToCheckPoint(Checkpoint checkpoint)
    {
        if (!respawn && !stoped)
        {
            this.checkpoint = checkpoint;
            deadParticles.Play();

            line.enabled = false;
            respawn = true;
            SetRigibody(false);

            OnBallShot.Invoke();
        }
    }
    #endregion

    #region Outputs
    #endregion
}
