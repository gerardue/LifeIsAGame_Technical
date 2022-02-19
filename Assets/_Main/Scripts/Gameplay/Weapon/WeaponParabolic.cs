using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for parabolic weapon
/// </summary>
public class WeaponParabolic : MonoBehaviour, IWeapon
{
    [Header("Setup Weapon")]
    [SerializeField]
    private WeaponParabolicSetup weaponSetup;

    [Header("Start position bullet")]
    [SerializeField]
    private Transform startPositionBullet;

    private Vector3 endPositionBullet;

    private CancellationTokenSource cts;

    #region Unity Messages

    private void Awake()
    {
        cts = new CancellationTokenSource();
    }


    #endregion

    #region Public Methods

    /// <summary>
    /// Shot (From interface)
    /// </summary>
    [ContextMenu("Parabolic")]
    public void Shot(Bullet bullet)
    {
        ShotAsync(bullet).WrapErrors();
    }

    public void OnTriggerEnterFunction(GameObject element, Bullet buller)
    {

    }

    public void Hit(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public void Miss(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Shot bullet async
    /// </summary>
    private async Task ShotAsync(Bullet bullet)
    {
        cts = new CancellationTokenSource();

        float t = 0;
        float anim = 0; 

        endPositionBullet = Camera.main.transform.position + (Camera.main.transform.forward * weaponSetup.WeaponParabolicStats.MaxDistance);
    
        while (t < weaponSetup.WeaponParabolicStats.TimeDuration - 0.01f && !cts.IsCancellationRequested)
        {
            if(!bullet.gameObject.activeSelf)
            {
                cts.Cancel();
                return; 
            }

            t += Time.deltaTime;
            anim += Time.deltaTime;
            anim %= weaponSetup.WeaponParabolicStats.TimeDuration;

            bullet.transform.position = Parabolic(startPositionBullet.position, endPositionBullet, weaponSetup.WeaponParabolicStats.Height, 
                anim / weaponSetup.WeaponParabolicStats.TimeDuration, weaponSetup.WeaponParabolicStats.ParabolicCurve);

            await Task.Yield();
        }


        if(t >= weaponSetup.WeaponParabolicStats.TimeDuration - 0.01)
            bullet.OnMiss(bullet); 
    }

    #region Parabolic Movement

    /// <summary>
    /// Parabolic movement using an animation curve
    /// </summary>
    private Vector3 Parabolic(Vector3 start, Vector3 end, float height, float t, AnimationCurve parabolicCurve)
    {
        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, CurveParabolicOne(height, parabolicCurve.Evaluate(t)) + Mathf.Lerp(start.y, end.y, 0), mid.z);
    }

    /// <summary>
    /// Parabolic movement using an equation
    /// </summary>
    private Vector3 Parabolic(Vector3 start, Vector3 end, float height, float t)
    {
        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, CurveParabolicTwo(height, t) + Mathf.Lerp(start.y, end.y, 0), mid.z);
    }

    /////////////////////////////////////////////
    // *** I present two ways to create a parabolic movement

    /// <summary>
    /// Calculate parabolic, method one
    /// </summary>
    private float CurveParabolicOne(float height, float t) => height * t;

    /// <summary>
    /// Calculate parabolic, method two
    /// </summary>
    public float CurveParabolicTwo(float height, float t) => -4 * (height * (t * t)) + 4 * (height * t);



    #endregion

    #endregion
}
