using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrabolicTrajectory : MonoBehaviour
{
    public Slider sliderAngle;
    public Slider sliderDirection;
    public Slider sliderPower;

    public LineRenderer lineRenderer;

    public int resloution = 30;
    public float timeStep = 0.1f;

    public Transform launchPoint;
    public Transform pivotPoint;

    public float launchPower;
    public float launchAngle;
    public float launchDirection;
    public float gravity = -9.8f;

    public GameObject projectilePrefabs;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = resloution;

        sliderAngle.onValueChanged.AddListener(sliderAngleValue);
        sliderDirection.onValueChanged.AddListener(sliderDirectionValue);
        sliderPower.onValueChanged.AddListener(sliderPowerValue);

    }

    // Update is called once per frame
    void Update()
    {
        RenderTrajectory();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject temp = Instantiate(projectilePrefabs);
            LaunchProjectile(temp);
        }
    }

    void sliderAngleValue(float angle)
    {
        launchAngle = angle;
    }
    void sliderDirectionValue(float angle)
    {
        launchDirection = angle;
    }
    void sliderPowerValue(float power)
    {
        launchPower = power;
    }

    void RenderTrajectory()
    {
        Vector3[] points = new Vector3[resloution];

        for(int i = 0; i< resloution; i++)
        {
            float time = i * timeStep;
            points[i] = CalculatePositionAtTime(time);
        }

        lineRenderer.SetPositions(points);
    }

    Vector3 CalculatePositionAtTime(float time)
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        return launchPoint.position + new Vector3(x, y, z);
    }

    public void LaunchProjectile(GameObject projectile)
    {
        projectile.transform.position = launchPoint.position;
        projectile.transform.rotation = launchPoint.rotation;
        projectile.transform.SetParent(null);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if(rb != null)
        {
            rb.isKinematic = false;

            float launchAngleRad = Mathf.Deg2Rad * launchAngle;
            float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

            float initialVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
            float initialVelocityZ = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Sin(launchDirectionRad);
            float initialVelocityY = launchPower * Mathf.Sin(launchAngleRad);

            Vector3 initalVelocity = new Vector3(initialVelocityX, initialVelocityY, initialVelocityZ);

            rb.velocity = initalVelocity;
        }

    }

}
