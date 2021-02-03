using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private bool mouseInButton = false;
    private bool mouseInButtonInPreFrame = false;

    private LevelManager levelManager = null;
    private PlayerMovement player = null;

    private void Start()
    {
        InicializeVariables();
        SubscribeToEvents();
    }

    private void InicializeVariables()
    {
        levelManager = GetComponentInParent<LevelManager>();
    }

    private void SubscribeToEvents()
    {
        levelManager.OnPlayerCreate.AddListener(SetPlayer);
    }

    private void SetPlayer()
    {
        player = levelManager.GetPlayer();
    }

    private void SetMouseInButton()
    {
        mouseInButton = true;
    }

    private void Update()
    {
        if (mouseInButton != mouseInButtonInPreFrame)
        {
            if (player != null)
            {
                if (mouseInButton) player.OnMouseInButton.Invoke();
                else player.OnNoMouseInButton.Invoke();
            }
        }

        print(mouseInButton);

        mouseInButtonInPreFrame = mouseInButton;
        mouseInButton = false;
    }
}
