using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimLine : MonoBehaviour
{

    private PlayerMovement playerMovement = null;
    private LineRenderer lineRenderer = null;

    [SerializeField]
    private Color maxColor = Color.white;
    [SerializeField]
    private Color minColor = Color.white;

    private void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
        lineRenderer = GetComponent<LineRenderer>();

        SubscribeToEvent();
    }

    private void SubscribeToEvent()
    {
        // No se debe desubscribir cuando esta desactivado.
        playerMovement.OnShotStart.AddListener(Activate);
        playerMovement.OnShotEnd.AddListener(Desactivate);
    }

    private void Activate()
    {
        this.enabled = true;
    }
    private void Desactivate()
    {
        this.enabled = false;
        SetLinePosition(Vector3.zero, Vector3.zero);
    }

    private void Update()
    {
        Color resultColor = Color.Lerp(minColor, maxColor, playerMovement.CalculateImpulseMultiplier());
        lineRenderer.startColor = resultColor;
        lineRenderer.endColor = resultColor;

        Vector3 pointPosition = transform.position + playerMovement.GetImpulseVector();

        SetLinePosition(transform.position, pointPosition);
    }

    private void SetLinePosition(Vector3 posA, Vector3 posB)
    {
        lineRenderer.SetPosition(0, posA);
        lineRenderer.SetPosition(1, posB);
    }

    //[SerializeField]
    //private PlayerMovement playerMovement = null;
    //[SerializeField]
    //private Color maxColor = Color.white;
    //[SerializeField]
    //private Color minColor = Color.white;

    //private LineRenderer lineRenderer = null;

    //private void Start()
    //{
    //    lineRenderer = GetComponent<LineRenderer>();
    //}

    //private void Update()
    //{
    //    if(playerMovement.currentShotState == PlayerMovement.ShotState.Start)
    //    {
    //        OnShotStart();
    //    }
    //    else if(playerMovement.currentShotState == PlayerMovement.ShotState.Charge)
    //    {
    //        OnShotHold();
    //    }
    //    else if(playerMovement.currentShotState == PlayerMovement.ShotState.Release)
    //    {
    //        OnShotRelease();
    //    }
    //}

    //private void OnShotStart()
    //{
    //    lineRenderer.enabled = true;
    //}

    //private void OnShotHold()
    //{
    //    Color resultColor = Color.Lerp(minColor, maxColor, playerMovement.CalculateImpulseMultiplier());
    //    lineRenderer.startColor = resultColor;
    //    lineRenderer.endColor = resultColor;

    //    lineRenderer.SetPosition(0, playerMovement.transform.position);
    //    lineRenderer.SetPosition(1, playerMovement.transform.position + playerMovement.GetImpulseVector());
    //}

    //private void OnShotRelease()
    //{
    //    lineRenderer.enabled = false;
    //}
}
