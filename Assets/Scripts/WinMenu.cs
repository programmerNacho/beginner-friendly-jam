using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText = null;
    [SerializeField]
    private TextMeshProUGUI shotText = null;
    [SerializeField]
    private TextMeshProUGUI deathText = null;
    [SerializeField]
    private GameObject winPanel = null;
    [SerializeField]
    private TextMeshProUGUI bestTimeText = null;
    [SerializeField]
    private TextMeshProUGUI bestShotText = null;
    [SerializeField]
    private TextMeshProUGUI bestDeathText = null;
    [SerializeField]
    private List<GameObject> toDeactive = new List<GameObject>();

    private LevelStats levelStats = null;

    private void Start()
    {
        levelStats = FindObjectOfType<LevelStats>();
        FindObjectOfType<LevelManager>().OnLevelCompleted.AddListener(StartWindow);
    }

    [ContextMenu("Open Win Menu")]
    public void StartWindow()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        int playTime = (int)levelStats.playTime;
        int numberOfShots = levelStats.NumberOfShots;
        int numberOfDeaths = levelStats.NumberOfDeaths;
        timeText.text = ConvertSecondsToStringFormatted(playTime);
        shotText.text = numberOfShots.ToString();
        deathText.text = numberOfDeaths.ToString();

        foreach (GameObject go in toDeactive)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        BestStats bs = BestStats.Instance;

        bestTimeText.text = ConvertSecondsToStringFormatted((int)bs.bestPlayTime);
        bestShotText.text = bs.bestNumberOfShots.ToString();
        bestDeathText.text = bs.bestNumberOfDeaths.ToString();
    }

    private string ConvertSecondsToStringFormatted(int seconds)
    {
        int minutes = (int)(seconds / 60f);
        int sec = seconds - (minutes * 60);
        string secondsString = sec < 10 ? "0" + sec.ToString() : sec.ToString();
        return minutes + ":" + secondsString;
    }
}
