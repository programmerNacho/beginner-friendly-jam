using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera initialVirtualCamera = null;

    private CinemachineVirtualCamera[] virtualCameras = null;

    private CinemachineVirtualCamera currentVirtualCamera = null;

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

    public void ChangeVirtualCamera(CinemachineVirtualCamera newVirtualCamera)
    {
        if(currentVirtualCamera != null)
        {
            currentVirtualCamera.gameObject.SetActive(false);
        }

        currentVirtualCamera = newVirtualCamera;

        currentVirtualCamera.gameObject.SetActive(true);
    }
}
