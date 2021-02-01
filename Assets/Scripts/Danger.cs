using UnityEngine;
using UnityEngine.Events;

public class Danger : MonoBehaviour
{
    CheckpointEvent ballKill;

    private void Start()
    {
        ballKill = FindObjectOfType<CheckpointManager>().OnPlayerReachedCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>())
        {
            ballKill.Invoke(FindObjectOfType<CheckpointManager>().LastCheckpoint);
        }
    }
}
