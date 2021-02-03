using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillY : MonoBehaviour
{
    [SerializeField]
    private float minY = -10f;
    private PlayerMovement playerMovement = null;

    private void Update()
    {
        if(playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
        else
        {
            if(playerMovement.transform.position.y <= minY)
            {
                FindObjectOfType<LevelManager>().OnPlayerDead.Invoke();
            }
        }
    }
}
