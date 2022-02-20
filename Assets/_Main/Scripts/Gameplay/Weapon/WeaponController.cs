using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for handle current weapon
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Default weapon")]
    [SerializeField]
    private Weapon defaultWeapon;

    [Header("Pool bullets")]
    [SerializeField]
    private List<Bullet> bullets = new List<Bullet>();

    [Header("Position Weapon")]
    [SerializeField]
    private Transform parentWeapon;
    [SerializeField]
    private Transform posWeapon;

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
        //currentWeapon = defaultWeapon.GetComponent<IWeapon>();
    }

    private void Update()
    {
        if (currentSpawnTime > 0)
        {
            currentSpawnTime -= Time.deltaTime;
            return;
        }

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
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
        if (other.CompareTag("Weapon"))
        {
            cts = new CancellationTokenSource();
            GetWeapon(other).WrapErrors();
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
    private async Task GetWeapon(Collider other)
    {
        while (!cts.IsCancellationRequested)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (currentWeapon != null)
                {
                    AnimationDropWeapon(currentWeapon.WeaponObject);
                }

                currentWeapon = other.GetComponent<IWeapon>();
                AnimationGetWeapon(other.gameObject);
                cts.Cancel();
            }
            await Task.Yield();
        }
    }

    /// <summary>
    /// Animation when pick up a weapon
    /// </summary>
    private void AnimationGetWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(parentWeapon);
        List<ICommand> commands = new List<ICommand>()
        {
            new Move(weapon, posWeapon.localPosition, 0.5f, isLocal: true),
            new Rotate(weapon, posWeapon.localEulerAngles, 0.5f, isLocal: true)
        };

        CommandQueue.Instance.ExecuteCommands(commands);
    }

    private void AnimationDropWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(null); 
        Vector3 newPos = weapon.transform.localPosition + (Vector3.down * 1.12f);

        ICommand command = new Move(weapon, newPos, 0.5f);
        CommandQueue.Instance.ExecuteCommand(command); 
    }

    #endregion
}
