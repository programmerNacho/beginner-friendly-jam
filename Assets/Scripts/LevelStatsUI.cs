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

    private string ConvertSecondsToStringFormatted(int seconds)
    {
        int minutes = (int)(seconds / 60f);
        int sec = seconds - (minutes * 60);
        string secondsString = sec < 10 ? "0"+ sec.ToString() : sec.ToString();
        return minutes + ":" + secondsString;
    }

    public void SetTimeText(int seconds)
    {
        timeText.text = ConvertSecondsToStringFormatted(seconds);
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
