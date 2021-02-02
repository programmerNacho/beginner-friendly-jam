using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField]
    private Transform transformToAlignHitParticle = null;
    [SerializeField]
    private PlayerMovement playerMovement = null;

    private ParticleSystem hitParticle = null;

    private void Start()
    {
        hitParticle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        AlignHitParticle();
        PlayHitParticle();
    }

    private void AlignHitParticle()
    {
        transformToAlignHitParticle.forward = -(playerMovement.GetImpulseVector().normalized);
    }

    private void PlayHitParticle()
    {
        if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        {
            hitParticle.Play();
        }
    }
}
