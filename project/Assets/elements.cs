using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCalculator : MonoBehaviour
{
    public float semiMajorAxis = 5f;   // a
    public float eccentricity = 0.5f; // e
    public float inclination = 0f;    // i in radians
    public float ascendingNode = 0f;  // Ω in radians
    public float periapsis = 0f;      // ω in radians
    public float gravitationalParameter = 1f; // GM of the central body

    private float meanAnomalyAtEpoch = 0f; // M0 in radians
    private float orbitalPeriod;
    private float currentTime;

    void Start()
    {
        // Calculate orbital period using Kepler's Third Law
        orbitalPeriod = 2 * Mathf.PI * Mathf.Sqrt(Mathf.Pow(semiMajorAxis, 3) / gravitationalParameter);
    }

    void Update()
    {
        // Increment time
        currentTime += Time.deltaTime;

        // Step 1: Calculate Mean Anomaly (M)
        float meanMotion = Mathf.Sqrt(gravitationalParameter / Mathf.Pow(semiMajorAxis, 3));
        float meanAnomaly = meanAnomalyAtEpoch + meanMotion * currentTime;

        // Step 2: Solve Kepler's Equation for Eccentric Anomaly (E)
        float eccentricAnomaly = SolveEccentricAnomaly(meanAnomaly, eccentricity);

        // Step 3: Calculate True Anomaly (v)
        float trueAnomaly = 2 * Mathf.Atan2(
            Mathf.Sqrt(1 + eccentricity) * Mathf.Sin(eccentricAnomaly / 2),
            Mathf.Sqrt(1 - eccentricity) * Mathf.Cos(eccentricAnomaly / 2)
        );

        // Step 4: Calculate Orbital Radius (r)
        float radius = semiMajorAxis * (1 - eccentricity * eccentricity) / (1 + eccentricity * Mathf.Cos(trueAnomaly));

        // Step 5: Calculate Cartesian Coordinates (x, y, z)
        Vector3 position = CalculateCartesianCoordinates(radius, trueAnomaly);

        // Debug or apply position to a GameObject
        Debug.Log($"Position: {position}");
    }

    float SolveEccentricAnomaly(float meanAnomaly, float eccentricity)
    {
        float tolerance = 1e-6f;
        float E = meanAnomaly; // Initial guess
        float delta;

        do
        {
            delta = (E - eccentricity * Mathf.Sin(E) - meanAnomaly) / (1 - eccentricity * Mathf.Cos(E));
            E -= delta;
        } while (Mathf.Abs(delta) > tolerance);

        return E;
    }

    Vector3 CalculateCartesianCoordinates(float radius, float trueAnomaly)
    {
        float xOrbital = radius * Mathf.Cos(trueAnomaly);
        float yOrbital = radius * Mathf.Sin(trueAnomaly);

        // Apply rotation using orbital elements (i, Ω, ω)
        float x = xOrbital * (Mathf.Cos(ascendingNode) * Mathf.Cos(periapsis) -
                              Mathf.Sin(ascendingNode) * Mathf.Sin(periapsis) * Mathf.Cos(inclination)) -
                  yOrbital * (Mathf.Cos(ascendingNode) * Mathf.Sin(periapsis) +
                              Mathf.Sin(ascendingNode) * Mathf.Cos(periapsis) * Mathf.Cos(inclination));

        float y = xOrbital * (Mathf.Sin(ascendingNode) * Mathf.Cos(periapsis) +
                              Mathf.Cos(ascendingNode) * Mathf.Sin(periapsis) * Mathf.Cos(inclination)) +
                  yOrbital * (Mathf.Cos(ascendingNode) * Mathf.Cos(periapsis) -
                              Mathf.Sin(ascendingNode) * Mathf.Sin(periapsis) * Mathf.Cos(inclination));

        float z = xOrbital * Mathf.Sin(periapsis) * Mathf.Sin(inclination) +
                  yOrbital * Mathf.Cos(periapsis) * Mathf.Sin(inclination);

        return new Vector3(x, z, y); // Unity uses y-up; swap y and z for Unity coordinate system
    }
}
