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
    private List<GameObject> toDeactive = new List<GameObject>();

    private LevelStats levelStats = null;

    private void Start()
    {
        levelStats = FindObjectOfType<LevelStats>();
    }

    [ContextMenu("Open Win Menu")]
    public void StartWindow()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        int playTime = (int)levelStats.playTime;
        int numberOfShots = levelStats.NumberOfShots;
        int numberOfDeaths = levelStats.NumberOfDeaths;
        timeText.text = playTime.ToString();
        shotText.text = numberOfShots.ToString();
        deathText.text = numberOfDeaths.ToString();

        foreach (GameObject go in toDeactive)
        {
            go.SetActive(false);
        }
    }
}
