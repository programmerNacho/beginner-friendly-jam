using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats : MonoBehaviour
{
    private PlayerMovement playerMovement = null;

    private TimeRegister timeRegister = null;

    private int numberOfShots = 0;

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

        timeRegister = new TimeRegister();
    }

    private void Update()
    {
        if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        {
            ShotTaken();
        }
    }

    [ContextMenu("Take Shot")]
    public void ShotTaken()
    {
        if(numberOfShots == 0)
        {
            timeRegister.Start();
        }

        numberOfShots++;
    }

    public void LevelFinished()
    {
        timeRegister.Stop();
    }

    public int GetSeconds()
    {
        return (int)timeRegister.Seconds;
    }
}
