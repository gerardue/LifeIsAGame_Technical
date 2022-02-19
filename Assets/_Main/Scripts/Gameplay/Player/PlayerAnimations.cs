using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Components")]
    public Animator animator;
    public PlayerHandler playerController;

    //Animations ID
    private int walkParameterId;

    private void Start()
    {
        walkParameterId = Animator.StringToHash("Walk");
    }

    private void Update()
    {
        WalkAnimation(playerController.move.magnitude);
    }

    public void WalkAnimation(float value)
    {
        animator.SetFloat(walkParameterId, value);
    }
}
