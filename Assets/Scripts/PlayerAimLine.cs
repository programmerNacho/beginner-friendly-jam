using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimLine : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement = null;
    [SerializeField]
    private Color maxColor = Color.white;
    [SerializeField]
    private Color minColor = Color.white;

    private LineRenderer lineRenderer = null;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(playerMovement.currentShotState == PlayerMovement.ShotState.Start)
        {
            OnShotStart();
        }
        else if(playerMovement.currentShotState == PlayerMovement.ShotState.Charge)
        {
            OnShotHold();
        }
        else if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
        {
            OnShotRelease();
        }
    }

    private void OnShotStart()
    {
        lineRenderer.enabled = true;
    }

    private void OnShotHold()
    {
        Color resultColor = Color.Lerp(minColor, maxColor, playerMovement.CalculateImpulseMultiplier());
        lineRenderer.startColor = resultColor;
        lineRenderer.endColor = resultColor;

        lineRenderer.SetPosition(0, playerMovement.transform.position);
        lineRenderer.SetPosition(1, playerMovement.transform.position + playerMovement.GetImpulseVector());
    }

    private void OnShotRelease()
    {
        lineRenderer.enabled = false;
    }
}
