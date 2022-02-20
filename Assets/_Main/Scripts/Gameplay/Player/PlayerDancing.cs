using System;
using UnityEngine;

/// <summary>
/// This class is responsible for run specific dance for character
/// </summary>
public class PlayerDancing : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private string nameAnimation;

    private Action<string> onSelectDance;

    public string NameAnimation { get => nameAnimation; set => nameAnimation = value; }

    #region Unity Message

    private void Start()
    {
        animator.Play("Base Layer." + NameAnimation);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Add event
    /// </summary>
    public void OnSelectDance(Action<string> onSelectDance) => this.onSelectDance = onSelectDance;
    
    /// <summary>
    /// Run event
    /// </summary>
    public void OnSelectDance() => onSelectDance?.Invoke(NameAnimation); 

    #endregion

}
