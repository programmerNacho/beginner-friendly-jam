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
    [SerializeField]
    private TextMeshProUGUI deathsText = null;

    private void Update()
    {
        SetTimeText((int)levelStats.playTime);
        SetShotsText(levelStats.NumberOfShots);
        SetDeathsText(levelStats.NumberOfDeaths);
        // Lo divido entre 2 porque me cuenta las muertes por duplicado.
    }

    public void SetTimeText(int seconds)
    {
        timeText.text = seconds.ToString();
    }

    public void SetShotsText(int shots)
    {
        shotsText.text = shots.ToString();
    }
    public void SetDeathsText(int deaths)
    {
        deathsText.text = deaths.ToString();
    }
}
