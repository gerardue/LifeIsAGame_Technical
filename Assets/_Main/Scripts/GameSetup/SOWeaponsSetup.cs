using UnityEngine;

/// <summary>
/// This scriptable object is responsible for the weapons stats
/// </summary>
[CreateAssetMenu(fileName = "WeaponParabolicSetup", menuName = "Weapons/Weapon Parabolic")]
public class SOWeaponsSetup : ScriptableObject
{
    [SerializeField]
    private WeaponParabolicStats weaponParabolicStats;
    [SerializeField]
    private WeaponOrbitalStats weaponOrbitalStats;
    [SerializeField]
    private WeaponWowStats weaponWowStats; 

    public WeaponParabolicStats WeaponParabolicStats { get => weaponParabolicStats; set => weaponParabolicStats = value; }
    public WeaponOrbitalStats WeaponOrbitalStats { get => weaponOrbitalStats; set => weaponOrbitalStats = value; }
    public WeaponWowStats WeaponWowStats { get => weaponWowStats; set => weaponWowStats = value; }
}

[System.Serializable]
public struct WeaponParabolicStats
{
    [SerializeField]
    private float height;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private AnimationCurve parabolicCurve;
    [SerializeField]
    private float timeDuration;

    public float Height { get => height; set => height = value; }
    public float MaxDistance { get => maxDistance; set => maxDistance = value; }
    public AnimationCurve ParabolicCurve { get => parabolicCurve; set => parabolicCurve = value; }
    public float TimeDuration { get => timeDuration; set => timeDuration = value; }
}

[System.Serializable]
public struct WeaponOrbitalStats
{
    [SerializeField]
    private float speedRotation;
    [SerializeField]
    private float radiusAttraction;
    [SerializeField]
    private float maxDistance; 

    public float SpeedRotation { get => speedRotation; set => speedRotation = value; }
    public float RadiusAttraction { get => radiusAttraction; set => radiusAttraction = value; }
    public float MaxDistance { get => maxDistance; set => maxDistance = value; }
}

[System.Serializable]
public struct WeaponWowStats
{
    [SerializeField]
    private float speedRotation;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float timeDurationCircleMove;

    public float SpeedRotation { get => speedRotation; set => speedRotation = value; }
    public float Radius { get => radius; set => radius = value; }
    public float MaxDistance { get => maxDistance; set => maxDistance = value; }
    public float TimeDurationCircleMove { get => timeDurationCircleMove; set => timeDurationCircleMove = value; }
}