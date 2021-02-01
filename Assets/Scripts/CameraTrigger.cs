using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;

    private CameraManager cameraManager = null;

    private void Start()
    {
        cameraManager = FindObjectOfType<CameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            cameraManager.ChangeVirtualCamera(virtualCamera);
        }
    }

    [ContextMenu("Heasdas")]
    private void Juejue()
    {
        cameraManager.ChangeVirtualCamera(virtualCamera);
    }
}
