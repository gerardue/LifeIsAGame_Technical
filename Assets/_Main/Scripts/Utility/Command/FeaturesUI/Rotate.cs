using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for rotating through commands
/// </summary>
public class Rotate : ICommand
{
    //Gameobject to animate
    private GameObject objectToAnimate;

    //Target position
    private Vector3 targetRotation;

    //Local Rotation
    private bool isLocal; 

    //Time duration animation
    private float time;

    //Time delay after finished animation
    private float startDelay;

    //Type animation to use
    private iTween.EaseType typeAnimation;

    //This reference to monobehaviour is used to run events when start or completed animation
    private MonoBehaviour monobehaviour;

    //Events
    private Action onStart;
    private Action onComplete;

    /// <summary>
    /// <para>
    /// Pass reference monobehaviour only if you going to use events. 
    /// To assign optional params pass them the follow way: "monoBehaviour : (your reference)" - "onStart : (your reference)" - "onComplete : (Your reference)"
    /// </para>
    /// </summary>
    public Rotate(GameObject objectToAnimate, Vector3 targetRotation, float time, bool isLocal = false, MonoBehaviour monoBehaviour = null, Action onStart = null, Action onComplete = null,
        float startDelay = 0, iTween.EaseType typeAnimation = iTween.EaseType.linear)
    {
        this.objectToAnimate = objectToAnimate;
        this.targetRotation = targetRotation;
        this.time = time;
        this.monobehaviour = monoBehaviour;
        this.onComplete = onComplete;
        this.onStart = onStart;
        this.typeAnimation = typeAnimation;
        this.startDelay = startDelay;
        this.isLocal = isLocal; 
    }

    public async Task ExecuteAsync()
    {
        objectToAnimate.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(startDelay));

        if (onStart != null && onComplete != null) //Run when exist events to onStart and onComplete
        {
            iTween.RotateTo(objectToAnimate, iTween.Hash("rotation", targetRotation, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart != null) //Run when exist events to onStart
        {
            iTween.RotateTo(objectToAnimate, iTween.Hash("rotation", targetRotation, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart));
        }
        else if (onComplete != null) //Run when exist events to onComplete
        {
            iTween.RotateTo(objectToAnimate, iTween.Hash("rotation", targetRotation, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart == null && onComplete == null) //Run when not exist events
        {
            iTween.RotateTo(objectToAnimate, iTween.Hash("rotation", targetRotation, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true));
        }

        await Task.Yield();
    }
}
