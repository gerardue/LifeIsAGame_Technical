using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for the behavior of the weapon Wow
/// </summary>
public class WeaponWow : Weapon, IWeapon
{
    private Vector3 endPositionBullet;

    private CancellationTokenSource cts;

    public GameObject WeaponObject { get => gameObject; }

    #region Unity Messages

    #endregion

    #region Public Methods

    #region IWeapon Interface

    public void Shot(Bullet bullet)
    {
        ShotAsync(bullet).WrapErrors();
    }

    public void Hit(Bullet bullet)
    {

    }

    public void Miss(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    public void OnTriggerEnterFunction(GameObject element, Bullet buller)
    {

    }

    #endregion

    #endregion

    #region Internal Methods

    private async Task ShotAsync(Bullet bullet)
    {
        cts = new CancellationTokenSource();

        try
        {
            bullet.SphereCollider.enabled = false;
            bullet.transform.position = startPositionBullet.position;
            bullet.SphereCollider.radius = weaponSetup.WeaponOrbitalStats.RadiusAttraction;

            endPositionBullet = startPositionBullet.position + (startPositionBullet.forward * weaponSetup.WeaponWowStats.MaxDistance);

            await MoveBullet(bullet, startPositionBullet.position, endPositionBullet, 1f);
            await CircularMovement(bullet);
            bullet.OnMiss(bullet);
        }
        catch { }
    }

    private async Task MoveBullet(Bullet bullet, Vector3 start, Vector3 end, float t)
    {
        float tempT = 0;
        try
        {
            while (tempT < t && !cts.IsCancellationRequested)
            {
                tempT += Time.deltaTime;
                Vector3 pos = Vector3.Lerp(start, end, tempT);
                bullet.transform.position = pos;
                await Task.Yield();
            }
        }
        catch { }
    }

    /// <summary>
    /// Circular movement for bullet
    /// </summary>
    private async Task CircularMovement(Bullet bullet)
    {
        Vector3 point = bullet.transform.position;
        float t = 0;

        try
        {
            while (t < weaponSetup.WeaponWowStats.TimeDurationCircleMove && !cts.IsCancellationRequested)
            {
                t += Time.deltaTime;
                bullet.transform.RotateAround(point + new Vector3(0, 0, weaponSetup.WeaponWowStats.Radius), Vector3.up, weaponSetup.WeaponWowStats.SpeedRotation * Time.deltaTime);
                await Task.Yield();
            }
        }
        catch { }
    }

    #endregion
}
