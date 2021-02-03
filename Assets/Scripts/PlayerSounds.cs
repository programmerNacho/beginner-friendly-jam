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
    private AudioClip hitSound = null;

    private void Start()
    {
        fxAudioSource = GetComponent<AudioSource>();
        playerMovement.OnBallMove.AddListener(PlaySound);
    }
    private void PlaySound()
    {
        fxAudioSource.pitch = Mathf.Lerp(minPitchHit, maxPitchHit, Random.Range(0f, 1f));
        fxAudioSource.PlayOneShot(hitSound);
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
