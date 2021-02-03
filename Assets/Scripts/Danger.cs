using UnityEngine;
using UnityEngine.Events;

public class Danger : MonoBehaviour
{
    private LevelManager levelManager = null;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnPlayerSpawn.AddListener(Activate);
    }

    private void Activate()
    {
        enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>())
        {
            enabled = false;
            levelManager.OnKillPlayer.Invoke();
        }
    }
}
