using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponAttractionGravity : MonoBehaviour, IWeapon
{
    [Header("Setup")]
    [SerializeField]
    private WeaponParabolicSetup weaponSetup;

    [SerializeField]
    private Transform startPositionBullet; 
    private Vector3 endPositionBullet;

    public List<Transform> elements = new List<Transform>();
    public List<Orbit> orbits = new List<Orbit>();


    public float OrbitDegrees;

    #region Unity Messages

    private void Awake()
    {
        //elements.ForEach(x =>
        //{
        //    var orbit = new Orbit(x, bullet1.transform, weaponSetup.WeaponAttractionGravityStats.SpeedRotation);
        //    orbits.Add(orbit);
        //    StartCoroutine(orbit.OrbitCoroutine());
        //});

        //Debug.Log(Vector3.Distance(elements[0].position, bullet1.position) + " radio");
    }

    private void OnDisable()
    {
        orbits.ForEach(x => x.StopOrbit());
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shot (From interface)
    /// </summary>
    public void Shot(Bullet bullet)
    {
        ShotAsync(bullet).WrapErrors();
    }

    public void Hit(Bullet bullet)
    {
        
    }

    public void Miss(Bullet bullet)
    {
        
    }


    public void OnTriggerEnterFunction(GameObject gameObject, Bullet bullet)
    {
        if (gameObject.CompareTag("Orbit"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            Orbit orbit = new Orbit(gameObject.transform, bullet.transform, weaponSetup.WeaponAttractionGravityStats.SpeedRotation);
            StartCoroutine(orbit.OrbitCoroutine());
        }
    }

    #endregion

    #region Private Methods

    private async Task ShotAsync(Bullet bullet)
    {
        bullet.SphereCollider.enabled = false;
        bullet.transform.position = startPositionBullet.position; 
        bullet.SphereCollider.radius = weaponSetup.WeaponAttractionGravityStats.RadiusAttraction; 

        endPositionBullet = Camera.main.transform.position + (Camera.main.transform.forward * weaponSetup.WeaponAttractionGravityStats.MaxDistance);

        await MoveBullet(bullet, startPositionBullet.position, endPositionBullet, 1f);

        bullet.SphereCollider.enabled = true;
    }

    private async Task MoveBullet(Bullet bullet, Vector3 start, Vector3 end, float t)
    {
        float tempT = 0;
        while(tempT < t)
        {
            tempT += Time.deltaTime;
            Vector3 pos = Vector3.Lerp(start, end, tempT);
            bullet.transform.position = pos; 
            await Task.Yield();
        }
    }

    #endregion

    #region Orbit Movement

    public class Orbit
    {
        private Transform element;
        private Transform bullet;

        private Vector3 orbit;
        private float speedRot;

        private CancellationTokenSource cts;

        public Orbit(Transform element, Transform bullet, float speedRot)
        {
            this.element = element;
            this.bullet = bullet;
            this.speedRot = speedRot;
            orbit = CalculateOrbit();
            cts = new CancellationTokenSource();
        }

        public IEnumerator OrbitCoroutine()
        {
            while (true)
            {
                element.transform.RotateAround(bullet.position, orbit, speedRot * Time.deltaTime);
                yield return null;
            }
        }

        /// <summary>
        /// Calculate Orbit
        /// </summary>
        private Vector3 CalculateOrbit()
        {
            //Transform position of target in degrees and then convert to Radians 
            float degrees = (Mathf.Atan2(element.position.x- bullet.position.x,element.position.z - bullet.position.z) * Mathf.Rad2Deg);
            float radians = (degrees) * Mathf.Deg2Rad;

            // Convert radian to cartesian plane
            var x = Mathf.Sin(-radians);
            var y = Mathf.Cos(radians);
            var z = Mathf.Tan(radians);

            return new Vector3(x, z, y);
        }

        /// <summary>
        /// Stop orbit movement
        /// </summary>
        public void StopOrbit()
        {
            cts.Cancel();
        }
    } 

    #endregion
}
