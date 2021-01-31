using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private List<Checkpoint> checkpoints = new List<Checkpoint>();

    public Checkpoint InitialCheckpoint
    {
        get;
        private set;
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
        if (checkpoints.Count == 0)
        {
            Debug.LogError("No Checkpoint assigned in list.");
            return;
        }

        InitialCheckpoint = LastCheckpoint = checkpoints[0];
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
