using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera initialVirtualCamera = null;

    private CinemachineVirtualCamera currentVirtualCamera = null;

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
