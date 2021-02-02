using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Map : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera startVirtualCamera = null;
    [SerializeField] Transform startSpawnPoint = null;

    CinemachineVirtualCamera currentVirtualCamera = null;
    Transform currentSpawnPoint = null;

    private void Start()
    {
        InicialiceVariables();
    }

    void InicialiceVariables()
    {
        currentVirtualCamera = startVirtualCamera;
        currentSpawnPoint = startSpawnPoint;
    }

    public void PlayerRespawnt()
    {

    }

    public CinemachineVirtualCamera GetCurrentVirtualCamera()
    {
        return currentVirtualCamera;
    }
    public Transform GetCurrentSpawnPoint()
    {
        return currentSpawnPoint;
    }
}
