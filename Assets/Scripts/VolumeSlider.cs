using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum TypeOfVolume { FX, Music }

    public TypeOfVolume typeOfVolume = TypeOfVolume.FX;

    private Slider slider = null;

    private void Start()
    {
        slider = GetComponent<Slider>();

        switch (typeOfVolume)
        {
            case TypeOfVolume.FX:
                slider.value = 1;
                break;
            case TypeOfVolume.Music:
                slider.value = 1;
                break;
        }
    }

    public void SetVolume(float value)
    {
        float exponentialValue = Mathf.Log10(value) * 20.0f;
        switch (typeOfVolume)
        {
            case TypeOfVolume.FX:
                SoundManager.Instance.VolumeFX = exponentialValue;
                break;
            case TypeOfVolume.Music:
                SoundManager.Instance.VolumeMusic = exponentialValue;
                break;
        }
    }

    public void SetMusicValue(float value)
    {
        float exponentialValue = Mathf.Log10(value) * 20.0f;
        Debug.Log(exponentialValue);
        SoundManager.Instance.VolumeMusic = exponentialValue;
    }
}
