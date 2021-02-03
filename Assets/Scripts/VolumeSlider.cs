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
                slider.value = SoundManager.Instance.VolumeFX;
                break;
            case TypeOfVolume.Music:
                slider.value = SoundManager.Instance.VolumeMusic;
                break;
        }
    }

    public void SetVolume(float value)
    {
        switch (typeOfVolume)
        {
            case TypeOfVolume.FX:
                SoundManager.Instance.VolumeFX = value;
                break;
            case TypeOfVolume.Music:
                SoundManager.Instance.VolumeMusic = value;
                break;
        }
    }
}
