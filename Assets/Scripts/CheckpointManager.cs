using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private Checkpoint initialCheckpoint = null;

    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    public Checkpoint InitialCheckpoint
    {
        get
        {
            return initialCheckpoint;
        }

        private set
        {
            initialCheckpoint = value;
        }
    }

    public Checkpoint LastCheckpoint
    {
        get;
        private set;
    }

    public CheckpointEvent OnPlayerReachedCheckpoint = new CheckpointEvent();

    private void Start()
    {
        InitializeVariables();
        SubscribeToCheckpoints();
    }

    private void InitializeVariables()
    {
        checkpoints = new List<Checkpoint>(FindObjectsOfType<Checkpoint>());

        if (checkpoints.Count == 0)
        {
            Debug.LogError("No Checkpoint assigned in list.");
            return;
        }

        LastCheckpoint = initialCheckpoint;
    }

    private void SubscribeToCheckpoints()
    {
        if (checkpoints.Count == 0)
        {
            Debug.LogError("No Checkpoint assigned in list.");
            return;
        }

        foreach (Checkpoint c in checkpoints)
        {
            c.OnPlayerEntered.AddListener(PlayerReachedCheckpoint);
        }
    }

    private void PlayerReachedCheckpoint(Checkpoint checkpoint)
    {
        LastCheckpoint = checkpoint;
        OnPlayerReachedCheckpoint.Invoke(LastCheckpoint);
    }
}
