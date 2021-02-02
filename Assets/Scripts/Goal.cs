using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent OnPlayerEntered = null;

    private void Start()
    {
        InicialiceVariables();
    }
    void InicialiceVariables()
    {
        OnPlayerEntered = FindObjectOfType<LevelManager>().OnMapCompleted;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            OnPlayerEntered.Invoke();
        }
    }
}
