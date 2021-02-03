using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem spawnParticles = null;
    [SerializeField]
    private ParticleSystem deathParticles = null;

    PlayerMovement playerMovement = null;
    Animator animator = null;

    private void Awake()
    {
        InicializeVariables();
        SubscribeToEvents();
    }

    void InicializeVariables()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    void SubscribeToEvents()
    {
        playerMovement.OnSpawnStart.AddListener(Spawn);
        playerMovement.OnDisappearStart.AddListener(Disappear);
    }
    public void Spawn()
    {
        animator.SetBool("isAlive", true);
        spawnParticles.Play();

    }
    public void Disappear()
    {
        animator.SetBool("isAlive", false);
        deathParticles.Play();
    }
    public void OnSpawnAnimationIsEnding()
    {
        playerMovement.OnSpawnEnd.Invoke();
    }
    public void OnDisappearAnimationIsEnding()
    {
        playerMovement.OnDisappearEnd.Invoke();
    }
}
