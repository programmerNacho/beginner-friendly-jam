using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

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
    }

    [SerializeField]
    private AudioMixer gameMixer = null;

    private float volumeFX = 0f;
    private float volumeMusic = 0f;

    public float VolumeFX
    {
        get
        {
            return volumeFX;
        }

        set
        {
            volumeFX = Mathf.Clamp(value, -80f, 0f);
            gameMixer.SetFloat("VolumeFX", volumeFX);
        }
    }

    public float VolumeMusic
    {
        get
        {
            return volumeMusic;
        }

        set
        {
            volumeMusic = Mathf.Clamp(value, -80f, 0f);
            gameMixer.SetFloat("VolumeMusic", volumeMusic);
        }
    }
}
