using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats : MonoBehaviour
{
    private PlayerMovement playerMovement = null;

    public float playTime = 0f;

    private int numberOfShots = 0;

    private bool levelFinished = false;

    public int NumberOfShots
    {
        get
        {
            return numberOfShots;
        }

        private set
        {
            numberOfShots = value;
        }
    }

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        {
            ShotTaken();
        }

        if(numberOfShots > 0 && !levelFinished)
        {
            playTime += Time.deltaTime;
        }
    }

    [ContextMenu("Take Shot")]
    public void ShotTaken()
    {
        numberOfShots++;
    }

    public void LevelFinished()
    {
        levelFinished = true;
    }
}
