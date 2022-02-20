using UnityEngine;

/// <summary>
/// This scriptable object is responsible for de game data
/// </summary>
[CreateAssetMenu(fileName = "GameDataSetup", menuName = "Game Data")]
public class SOGameData : ScriptableObject
{
    [SerializeField]
    private string nameAnimation;

    public string NameAnimation { get => nameAnimation; set => nameAnimation = value; }
}
