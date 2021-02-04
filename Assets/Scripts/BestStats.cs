using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestStats : MonoBehaviour
{
    public static BestStats Instance { get; private set; }

    public float bestPlayTime = 0;

    public int bestNumberOfShots = 0;

    public int bestNumberOfDeaths = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);

        bestPlayTime = float.MaxValue;
        bestNumberOfShots = int.MaxValue;
        bestNumberOfDeaths = int.MaxValue;
    }

    public void LevelFinished(float playTime, int numberOfShots, int numberOfDeaths)
    {
        bestPlayTime = playTime < bestPlayTime ? playTime : bestPlayTime;
        bestNumberOfShots = numberOfShots < bestNumberOfShots ? numberOfShots : bestNumberOfShots;
        bestNumberOfDeaths = numberOfDeaths < bestNumberOfDeaths ? numberOfDeaths : bestNumberOfDeaths;
    }
}
