using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for handle current weapon
/// </summary>
public class WeaponController : MonoBehaviour
{
    public WeaponParabolic weaponParabolic;
    public WeaponAttractionGravity weaponOrbit;

    [Header("Pool bullets")]
    [SerializeField]
    private List<Bullet> bullets = new List<Bullet>();

    private Queue<Bullet> bulletsToUse = new Queue<Bullet>();

    private IWeapon currentWeapon;

    //To Shot
    private float spawnTime = 0.5f;
    private float currentSpawnTime;
    
    private CancellationTokenSource cts;

    #region Unity Messages

    private void Awake()
    {
        bullets.ForEach(x => bulletsToUse.Enqueue(x)); 
        currentWeapon = weaponOrbit.GetComponent<IWeapon>();
    }

    private void Update()
    {
        if (currentSpawnTime > 0)
        {
            currentSpawnTime -= Time.deltaTime;
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            currentSpawnTime = spawnTime;

            Bullet bullet = bulletsToUse.Dequeue();
            bullet.OnShot = Shot; 
            bullet.OnHit = Hit;
            bullet.OnMiss = Miss; 
            bullet.TriggerEnter = TriggerEnter;
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            cts = new CancellationTokenSource();
            GetWeapon(other.GetComponent<IWeapon>()).WrapErrors(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            cts.Cancel();
        }
    }

    #endregion

    #region Public Methods

    

    #endregion

    #region Private Methods

    private void Shot(Bullet bullet)
    {
        currentWeapon?.Shot(bullet);
    }

    private void Hit(Bullet bullet)
    {
        bulletsToUse.Enqueue(bullet);
        currentWeapon?.Hit(bullet);
    }

    private void Miss(Bullet bullet)
    {
        bulletsToUse.Enqueue(bullet);
        currentWeapon!.Miss(bullet);
    }

    private void TriggerEnter(GameObject gameObject, Bullet bullet)
    {
        currentWeapon?.OnTriggerEnterFunction(gameObject, bullet);
    }

    /// <summary>
    /// Get new weapon or change weapon
    /// </summary>
    private async Task GetWeapon(IWeapon weapon)
    {
        while(!cts.IsCancellationRequested)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                currentWeapon = weapon;
            }
            await Task.Yield();
        }
    }

    #endregion
}
