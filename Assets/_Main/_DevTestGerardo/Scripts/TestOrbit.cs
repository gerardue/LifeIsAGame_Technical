using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOrbit : MonoBehaviour
{
    public float speedRot = 5;
    public float radius = 8;

    public Transform bullet;
    private Vector3 orbit;
    private float radiusDis;

    private void Awake()
    {
        radiusDis = (transform.position - bullet.transform.position).magnitude;
        orbit = CalculateOrbit();
    }

    private void Update()
    {
        transform.RotateAround(bullet.position * radiusDis, orbit, speedRot * Time.deltaTime);
    }

    public Vector3 CalculateOrbit()
    {
        //Transform position of target in degrees and then convert to Radians 
        float degrees = (Mathf.Atan2(transform.position.x - bullet.transform.position.x, transform.position.z - bullet.transform.position.z) * Mathf.Rad2Deg);
        float radians = (degrees - bullet.position.y) * Mathf.Deg2Rad;

        // Convert radian to cartesian plane
        var x = Mathf.Sin(-radians);
        var y = Mathf.Cos(radians);
        var z = Mathf.Tan(radians);

        return new Vector3(x, z, y); 
    }
}
