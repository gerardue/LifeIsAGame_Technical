using UnityEngine;

/// <summary>
/// This class is an abstraction for the weapon
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    protected SOWeaponsSetup weaponSetup;

    [SerializeField]
    protected Transform startPositionBullet;
}