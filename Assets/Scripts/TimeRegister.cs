using UnityEngine;

public class TimeRegister
{
    private bool isActive = false;
    private float startTime = 0f;
    private float stopTime = 0f;

    public bool IsActive
    {
        get
        {
            return isActive;
        }
    }

    public float Seconds
    {
        get
        {
            return isActive ? Time.time - startTime : stopTime - startTime;
        }
    }

    public TimeRegister()
    {
        isActive = false;
        startTime = 0f;
        stopTime = 0f;
    }

    public void Start()
    {
        isActive = true;

        startTime = Time.time;
    }

    public void Stop()
    {
        isActive = false;

        stopTime = Time.time;
    }
}
