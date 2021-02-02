using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStatsUI : MonoBehaviour
{
    [SerializeField]
    private LevelStats levelStats = null;

    [SerializeField]
    private TextMeshProUGUI timeText = null;
    [SerializeField]
    private TextMeshProUGUI shotsText = null;

    private void Update()
    {
        SetTimeText(levelStats.GetSeconds());
        SetShotsText(levelStats.NumberOfShots);
    }

    public void SetTimeText(int seconds)
    {
        timeText.text = seconds.ToString();
    }

    public void SetShotsText(int shots)
    {
        shotsText.text = shots.ToString();
    }
}
