using UnityEngine;
using UnityEngine.Events;

public class CheckpointEvent : UnityEvent<Checkpoint> { }

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint = null;

    public Transform SpawnPoint
    {
        get
        {
            return spawnPoint;
        }
    }

    public CheckpointEvent OnPlayerEntered = new CheckpointEvent();

    private void Start()
    {
        if(spawnPoint == null)
        {
            Debug.LogWarning("Spawn point was set to self.");
            spawnPoint = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMovement>())
        {
            OnPlayerEntered.Invoke(this);
        }
    }
}
