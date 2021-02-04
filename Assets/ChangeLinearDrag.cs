using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinearDrag : MonoBehaviour
{
    [Range(0, 10.0f)]
    [SerializeField]
    private float drag;

    private float oldDrag = 0;
    private float oldAngularDrag = 0;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rigi = null;
        if (other.TryGetComponent<Rigidbody>(out rigi))
        {
            oldDrag = rigi.drag;
            oldAngularDrag = rigi.angularDrag;
            rigi.drag = drag;
            rigi.angularDrag = drag;

            if (drag < 1) rigi.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigi = null;
        if (other.TryGetComponent<Rigidbody>(out rigi))
        {
            rigi.drag = oldAngularDrag;
            rigi.angularDrag = oldDrag;

            if (drag < 1) rigi.useGravity = true;
        }
    }
}
