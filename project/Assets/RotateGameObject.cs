using System.Collections.Generic;
using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    public float x = 0.1f;  // Rotation speed for Earth's rotation
    public float y = 0.1f;
    public float z = 0.1f;

    public Transform Moon;  // Reference to the Moon's Transform
    public Transform Earth; // Reference to the Earth's Transform

    private Vector3 EarthPosition = new Vector3(1500, 0, 0); // Earth's fixed position

    void Start()
    {
        // Set Earth's position
        if (Earth != null)
        {
            Earth.position = EarthPosition; 
        }

        // Set Moon's position relative to the Earth
        if (Moon != null && Earth != null)
        {
            Moon.position = Earth.position + new Vector3(4f, 0, 0);


        }
    }

    void Update()
    {
        // Rotate the object this script is attached to
        transform.Rotate(x, y, z);
    }
    
}
