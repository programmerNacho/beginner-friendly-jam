using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField]
    private LevelVisualizer levelVisualizer = null;
    [SerializeField]
    private CheckpointManager checkpointManager = null;
    [SerializeField]
    private Goal goal = null;
    [SerializeField]
    private GameObject player = null;

    private TimeRegister timeRegister = null;

    private int ballShotCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        checkpointManager.OnPlayerReachedCheckpoint.AddListener(PlayerReachedCheckpoint);
        goal.OnPlayerEntered.AddListener(PlayerReachedGoal);
    }

    private void OnDisable()
    {
        checkpointManager.OnPlayerReachedCheckpoint.RemoveListener(PlayerReachedCheckpoint);
        goal.OnPlayerEntered.RemoveListener(PlayerReachedGoal);
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        timeRegister = new TimeRegister();
        ballShotCount = 0;
    }

    public void BallShotTaken()
    {
        if (ballShotCount == 0)
        {
            timeRegister.Start();
        }

        ballShotCount++;
        levelVisualizer.Show();
    }

    private void PlayerReachedGoal()
    {
        timeRegister.Stop();
    }

    private void PlayerReachedCheckpoint(Checkpoint checkpoint)
    {
        RespawnPlayerInLastCheckpoint();
    }

    private void RespawnPlayerInLastCheckpoint()
    {
        player.transform.position = checkpointManager.LastCheckpoint.SpawnPoint.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        levelVisualizer.Show();
    }
}
