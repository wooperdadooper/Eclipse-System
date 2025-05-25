using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timecontroller : MonoBehaviour
{
    public float timeStep = 0.5f; // how much to increase/decrease per click
    public float maxTimeScale = 100f;
    public float minTimeScale = 0.1f;

    public void SpeedUp()
    {
        Time.timeScale = Mathf.Min(Time.timeScale + timeStep, maxTimeScale);
        Debug.Log("Speed: " + Time.timeScale);
    }

    public void SlowDown()
    {
        Time.timeScale = Mathf.Max(Time.timeScale - timeStep, minTimeScale);
        Debug.Log("Speed: " + Time.timeScale);
    }
}
