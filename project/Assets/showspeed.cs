using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showspeed : MonoBehaviour
{
    public TextMeshProUGUI speedText;  // Assign in Inspector

    void Update()
    {
        if (speedText != null)
        {
            speedText.text = "Speed: " + Time.timeScale.ToString("0.0");
        }
    }
}
