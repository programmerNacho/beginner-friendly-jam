using UnityEngine;
using UnityEngine.Events;

public class Danger : MonoBehaviour
{
    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>())
        {
            levelManager.OnPlayerDead.Invoke();
        }
    }
}
