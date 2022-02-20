using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for the behavior of the weapon parabolic
/// </summary>
public class WeaponParabolic : Weapon, IWeapon
{
    private Vector3 endPositionBullet;

    private CancellationTokenSource cts;

    public GameObject WeaponObject { get => gameObject; }

    #region Unity Messages

    private void Awake()
    {
        cts = new CancellationTokenSource();
    }


    #endregion

    #region Public Methods

    #region IWeapon Interface

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

    #endregion

    #region Private Methods

    private async Task ShotAsync(Bullet bullet)
    {
        cts = new CancellationTokenSource();

        float t = 0;
        float anim = 0;

        try
        {
            endPositionBullet = Camera.main.transform.position + (transform.forward * weaponSetup.WeaponParabolicStats.MaxDistance);

            while (t < weaponSetup.WeaponParabolicStats.TimeDuration - 0.01f && !cts.IsCancellationRequested)
            {
                if (!bullet.gameObject.activeSelf)
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
            bullet.OnMiss(bullet);
        }
        catch { }
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
