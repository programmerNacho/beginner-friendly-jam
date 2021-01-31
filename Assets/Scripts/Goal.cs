using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent OnPlayerEntered = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerMovement>())
        {
            OnPlayerEntered.Invoke();
        }
    }
}
