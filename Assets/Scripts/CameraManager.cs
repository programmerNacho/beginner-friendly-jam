using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public UnityEvent OnCameraBlendEnded = new UnityEvent();

    [SerializeField]
    private CinemachineVirtualCamera initialVirtualCamera = null;

    private CinemachineVirtualCamera[] virtualCameras = null;

    private CinemachineVirtualCamera lastVirtualCamera = null;
    private CinemachineVirtualCamera currentVirtualCamera = null;

    private bool movingCamera = false;

    private void Start()
    {
        if(initialVirtualCamera == null)
        {
            Debug.LogError("No initial virtual camera assigned.");
        }

        virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();

        foreach (CinemachineVirtualCamera c in virtualCameras)
        {
            if(c != initialVirtualCamera)
            {
                c.gameObject.SetActive(false);
            }
        }

        ChangeVirtualCamera(initialVirtualCamera);
    }

    private void Update()
    {
        MovingCamera();
    }

    void MovingCamera()
    {
        if (lastVirtualCamera != null)
        {
            if (movingCamera && !CinemachineCore.Instance.IsLive(lastVirtualCamera))
            {
                movingCamera = false;
                OnCameraBlendEnded.Invoke();
            }
        }
    }

    public void ChangeVirtualCamera(CinemachineVirtualCamera newVirtualCamera)
    {
        if(currentVirtualCamera != null)
        {
            currentVirtualCamera.gameObject.SetActive(false);
        }

        lastVirtualCamera = currentVirtualCamera;
        movingCamera = true;

        currentVirtualCamera = newVirtualCamera;

        currentVirtualCamera.gameObject.SetActive(true);
    }
}
