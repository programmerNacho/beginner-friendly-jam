using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Rigidbody rigibody = null;
    PlayerInput playerInput = null;
    PlayerMovement playerMovement = null;
    Animator animator = null;

    private void Awake()
    {
        InicializeVariables();
    }

    void InicializeVariables()
    {
        rigibody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }
    public void Spawn()
    {
        animator.SetBool("isAlive", true);
    }
    public void Despawn()
    {
        animator.SetBool("isAlive", false);
        playerMovement.controllable = false;
    }

    public void OnSpawnAnimationIsEnding()
    {
        playerMovement.controllable = true;
    }
}
