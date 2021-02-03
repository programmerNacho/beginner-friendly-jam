using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillY : MonoBehaviour
{
    [SerializeField]
    private float minY = -10f;
    private LevelManager levelManager = null;
    private PlayerMovement playerMovement = null;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnPlayerCreate.AddListener(SetPlayer);
        levelManager.OnPlayerSpawn.AddListener(Activate);
    }

    private void SetPlayer()
    {
        playerMovement = levelManager.GetPlayer();
    }

    private void Activate()
    {
        enabled = true;
    }

    private void Update()
    {
        if(playerMovement != null)
        {
            if(playerMovement.transform.position.y <= minY)
            {
                if (enabled)
                {
                    enabled = false;
                    KillPlayer();
                }
            }
        }
    }

    public void KillPlayer()
    {
        levelManager.OnKillPlayer.Invoke();
    }
}
