using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class mooninclinationchanger : MonoBehaviour

{
    public Transform moonOrbitParent; // The object that defines the Moon's orbital plane
    private float currentInclination = 5f;

    public void SetInclination(float degrees)
    {
        currentInclination = degrees;
        moonOrbitParent.localRotation = Quaternion.Euler(currentInclination, 0f, 0f);
    }

    public void IncreaseInclination()
    {
        SetInclination(currentInclination + 1f);
    }

    public void DecreaseInclination()
    {
        SetInclination(currentInclination - 1f);
    }
}

