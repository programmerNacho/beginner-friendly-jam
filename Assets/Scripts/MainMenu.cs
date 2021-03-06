using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera mainMenuCamera = null;
    [SerializeField]
    private CinemachineVirtualCamera loadSceneCamera = null;
    [SerializeField]
    private CinemachineVirtualCamera creditsCamera = null;
    [SerializeField]
    private float loadSceneAnimationTime = 2f;
    [SerializeField]
    private string sceneName = "";

    [SerializeField]
    private Animator whiteAnimator;

    private bool isLoadingLevel = false;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void StartLoadingLevel()
    {
        if(!isLoadingLevel)
        {
            isLoadingLevel = true;
            StartCoroutine(LoadSceneCoroutine());
        }
    }

    public void OpenCredits()
    {
        mainMenuCamera.gameObject.SetActive(false);
        creditsCamera.gameObject.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsCamera.gameObject.SetActive(false);
        mainMenuCamera.gameObject.SetActive(true);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
    private IEnumerator LoadSceneCoroutine()
    {
        mainMenuCamera.gameObject.SetActive(false);
        loadSceneCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(loadSceneAnimationTime);
        SceneManager.LoadScene(sceneName);
    }
}
