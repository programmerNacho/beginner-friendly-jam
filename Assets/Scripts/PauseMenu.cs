using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActionAsset;

    [SerializeField]
    private GameObject pauseMenu = null;

    private bool menuIsOpen = false;

    public void ActivateMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        menuIsOpen = true;
        inputActionAsset.Disable();
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        inputActionAsset.Enable();
        menuIsOpen = false;
    }

    private void Update()
    {
        if (menuIsOpen) inputActionAsset.Disable();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
