using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStatsUI : MonoBehaviour
{
    [SerializeField]
    private LevelStats levelStats = null;

    [SerializeField]
    private Animator shotAnimator = null;
    [SerializeField]
    private Animator shotTextAnimator = null;
    [SerializeField]
    private Animator deathAnimator = null;
    [SerializeField]
    private Animator deathTextAnimator = null;

    [SerializeField]
    private TextMeshProUGUI timeText = null;
    [SerializeField]
    private TextMeshProUGUI shotsText = null;
    [SerializeField]
    private TextMeshProUGUI deathsText = null;

    private void Start()
    {
        levelStats.OnChangeShots.AddListener(SetShotsText);
        levelStats.OnChangeDeaths.AddListener(SetDeathsText);
    }

    private void Update()
    {
        SetTimeText((int)levelStats.playTime);
        //SetShotsText(levelStats.NumberOfShots);
        //SetDeathsText(levelStats.NumberOfDeaths);
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

    public void SetShotsText()
    {
        shotsText.text = levelStats.NumberOfShots.ToString();
        shotAnimator.Play("UI Bounce", 0, 0);
        shotTextAnimator.Play("UI Bounce", 0, 0);
    }
    public void SetDeathsText()
    {
        deathsText.text = levelStats.NumberOfDeaths.ToString();
        deathAnimator.Play("UI Bounce", 0, 0);
        deathTextAnimator.Play("UI Bounce", 0, 0);
    }
}
