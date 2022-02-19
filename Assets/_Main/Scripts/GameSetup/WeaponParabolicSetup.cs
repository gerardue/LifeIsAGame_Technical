using UnityEngine;

[CreateAssetMenu(fileName = "WeaponParabolicSetup", menuName = "Weapons/Weapon Parabolic")]
public class WeaponParabolicSetup : ScriptableObject
{
    [SerializeField]
    private WeaponParabolicStats weaponParabolicStats;
    [SerializeField]
    private WeaponAttractionGravityStats weaponAttractionGravityStats;

    public WeaponParabolicStats WeaponParabolicStats { get => weaponParabolicStats; set => weaponParabolicStats = value; }
    public WeaponAttractionGravityStats WeaponAttractionGravityStats { get => weaponAttractionGravityStats; set => weaponAttractionGravityStats = value; }
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
public struct WeaponAttractionGravityStats
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