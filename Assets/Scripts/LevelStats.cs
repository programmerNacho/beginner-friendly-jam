using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStats : MonoBehaviour
{
    private LevelManager levelManager = null;
    private PlayerMovement playerMovement = null;

    public UnityEvent OnChangeShots = new UnityEvent();
    public UnityEvent OnChangeDeaths = new UnityEvent();

    public float playTime = 0f;

    private int numberOfShots = 0;

    private int numberOfDeaths = 0;

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

    public int NumberOfDeaths
    {
        get
        {
            return numberOfDeaths;
        }

        private set
        {
            numberOfDeaths = value;
        }
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnPlayerCreate.AddListener(SetPlayer);
        levelManager.OnPlayerIsDead.AddListener(AddDeath);
        levelManager.OnLevelCompleted.AddListener(LevelFinished);
    }

    private void SetPlayer()
    {
        playerMovement = levelManager.GetPlayer();
        playerMovement.OnBallMove.AddListener(ShotTaken);
    }

    private void Update()
    {
        //if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        //{
        //    ShotTaken();
        //}

        if(numberOfShots > 0 && !levelFinished)
        {
            playTime += Time.deltaTime;
        }
    }

    public void ShotTaken()
    {
        numberOfShots++;
        OnChangeShots.Invoke();
    }

    private void AddDeath()
    {
        numberOfDeaths++;
        OnChangeDeaths.Invoke();
    }

    public void LevelFinished()
    {
        levelFinished = true;

        BestStats.Instance.LevelFinished(playTime, numberOfShots, numberOfDeaths);
    }
}
