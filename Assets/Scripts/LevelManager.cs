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
    private PlayerMovement player = null;

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
        player.OnBallShot.AddListener(BallShotTaken);
    }

    private void OnDisable()
    {
        checkpointManager.OnPlayerReachedCheckpoint.RemoveListener(PlayerReachedCheckpoint);
        goal.OnPlayerEntered.RemoveListener(PlayerReachedGoal);
        player.OnBallShot.RemoveListener(BallShotTaken);
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
        levelVisualizer.Hide();
    }

    private void PlayerReachedGoal()
    {
        timeRegister.Stop();
        print(timeRegister.Seconds);
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
