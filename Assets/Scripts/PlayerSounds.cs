using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement = null;
    [SerializeField]
    private float maxPitchHit = 1f;
    [SerializeField]
    private float minPitchHit = 0f;

    private AudioSource fxAudioSource = null;

    [SerializeField]
    private AudioClip shotSound = null;
    [SerializeField]
    private AudioClip collidedSound = null;
    [SerializeField]
    private AudioClip spawnSound = null;
    [SerializeField]
    private AudioClip disappearSound = null;

    private void Start()
    {
        fxAudioSource = GetComponent<AudioSource>();
        playerMovement.OnBallMove.AddListener(PlayShot);
        playerMovement.OnSpawnStart.AddListener(PlaySpawn);
        playerMovement.OnDisappearStart.AddListener(PlayDisappear);
        playerMovement.OnBallCollided.AddListener(PlayCollided);
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            fxAudioSource.pitch = Mathf.Lerp(minPitchHit, maxPitchHit, Random.Range(0f, 1f));
            fxAudioSource.PlayOneShot(clip);
        }
    }

    private void PlayShot()
    {
        PlaySound(shotSound);
    }
    private void PlaySpawn()
    {
        PlaySound(spawnSound);
    }
    private void PlayDisappear()
    {
        PlaySound(disappearSound);
    }
    private void PlayCollided()
    {
        PlaySound(collidedSound);
    }
    private void Update()
    {
        //if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        //{
        //    fxAudioSource.pitch = Mathf.Lerp(minPitchHit, maxPitchHit, Random.Range(0f, 1f));
        //    fxAudioSource.PlayOneShot(hitSound);
        //}
    }
}
