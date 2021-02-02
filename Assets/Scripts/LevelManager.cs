using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public UnityEvent OnPlayerSpawn = new UnityEvent();
    public UnityEvent OnPlayerDead = new UnityEvent();
    public UnityEvent OnMapCompleted = new UnityEvent();

    [SerializeField] GameObject playerPrefab = null;
    PlayerManager player;

    MapManager mapManager = null;
    CameraManager cameraManager = null;

    Transform currentSpawnPoint;
    LevelVisualizer levelVisualizer = null;
    CinemachineVirtualCamera currentVirtualCamera;

    private void Start()
    {
        InicializeVariables();

        SubscribeToEvent();

        Invoke("SetNewGame", 0.10f);
    }

    void InicializeVariables()
    {
        mapManager = FindObjectOfType<MapManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        levelVisualizer = GetComponent<LevelVisualizer>();
    }

    void SubscribeToEvent()
    {
        OnMapCompleted.AddListener(GoToNextMap);
        cameraManager.OnCameraBlendEnded.AddListener(PlayerSpawn);
    }

    void SetNewGame()
    {
        PrepareTheMap(mapManager.GetMap());
        CreatePlayer();
        MovePlayerToSpawnPoint();
        player.Spawn();
    }

    void PrepareTheMap(Map map)
    {
        if (map != null)
        {
            // Guardamos el spawn y camara
            currentSpawnPoint = map.GetCurrentSpawnPoint();

            // Reposiciona la camara
            currentVirtualCamera = map.GetCurrentVirtualCamera();
            cameraManager.ChangeVirtualCamera(currentVirtualCamera);
        }
        else
        {
            Victory();
        }
    }

    void CreatePlayer()
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        player = newPlayer.GetComponent<PlayerManager>();
    }
    void MovePlayerToSpawnPoint()
    {
        // Reposiciona al jugador
        player.transform.position = currentSpawnPoint.transform.position;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.Despawn();
    }

    void GoToNextMap()
    {
        PrepareTheMap(mapManager.GetNextMap());
        MovePlayerToSpawnPoint();
    }

    void PlayerSpawn()
    {
        player.Spawn();
    }
    void PlayerDespawn()
    {
        player.Despawn();
    }

    void Victory()
    {
        Debug.Log("Winner");
    }

    //public static LevelManager Instance { get; private set; }

    //[SerializeField]
    //private LevelVisualizer levelVisualizer = null;
    //[SerializeField]
    //private CheckpointManager checkpointManager = null;
    //[SerializeField]
    //private Goal goal = null;
    //[SerializeField]
    //private PlayerMovement player = null;

    //private TimeRegister timeRegister = null;

    //private int ballShotCount = 0;

    //private void Awake()
    //{
    //    if (Instance != null && Instance != this)
    //    {
    //        Destroy(gameObject);
    //    }

    //    else
    //    {
    //        Instance = this;
    //    }
    //}

    //private void OnEnable()
    //{
    //    checkpointManager.OnPlayerReachedCheckpoint.AddListener(PlayerReachedCheckpoint);
    //    goal.OnPlayerEntered.AddListener(PlayerReachedGoal);
    //    //player.OnShotReleased.AddListener(BallShotTaken);
    //}

    //private void OnDisable()
    //{
    //    checkpointManager.OnPlayerReachedCheckpoint.RemoveListener(PlayerReachedCheckpoint);
    //    goal.OnPlayerEntered.RemoveListener(PlayerReachedGoal);
    //   // player.OnShotReleased.RemoveListener(BallShotTaken);
    //}

    //private void Start()
    //{
    //    InitializeVariables();
    //}

    //private void InitializeVariables()
    //{
    //    timeRegister = new TimeRegister();
    //    ballShotCount = 0;
    //}

    //public void BallShotTaken()
    //{
    //    if (ballShotCount == 0)
    //    {
    //        timeRegister.Start();
    //    }

    //    ballShotCount++;
    //    levelVisualizer.Hide();
    //}

    //private void PlayerReachedGoal()
    //{
    //    timeRegister.Stop();
    //    print(timeRegister.Seconds);
    //}

    //private void PlayerReachedCheckpoint(Checkpoint checkpoint)
    //{
    //    RespawnPlayerInLastCheckpoint();
    //}

    //private void RespawnPlayerInLastCheckpoint()
    //{
    //    //player.transform.position = checkpointManager.LastCheckpoint.SpawnPoint.position;

    //   // player.ReturnToCheckPoint(checkpointManager.LastCheckpoint);
    //    player.GetComponent<Rigidbody>().velocity = Vector3.zero;
    //    levelVisualizer.Show();
    //}
}
