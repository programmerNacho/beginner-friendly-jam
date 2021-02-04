using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSound : MonoBehaviour
{
    private LevelManager levelManager;
    private AudioSource fxAudioSource = null;

    [SerializeField]
    private float maxPitchHit = 1f;
    [SerializeField]
    private float minPitchHit = 0f;

    [SerializeField]
    private AudioClip ballInHoleSound = null;
    [SerializeField]
    private AudioClip completeSound = null;
    [SerializeField]
    private AudioClip deadSound = null;

    private void Start()
    {
        fxAudioSource = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.OnMapCompleted.AddListener(PlayComplete);
        levelManager.OnPlayerIsDead.AddListener(PlayDead);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            fxAudioSource.pitch = Mathf.Lerp(minPitchHit, maxPitchHit, Random.Range(0f, 1f));
            fxAudioSource.PlayOneShot(clip);
        }
    }
    private void PlayComplete()
    {
        PlaySound(ballInHoleSound);
        PlaySound(completeSound);
    }
    private void PlayDead()
    {
        PlaySound(deadSound);
    }

}
