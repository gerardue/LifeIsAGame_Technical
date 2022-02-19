using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for shaking move through commands
/// </summary>
public class ShakePosition : ICommand
{
    //Gameobject to animate
    private GameObject objectToAnimate;

    //Target position
    private Vector3 shakePosition;

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
    public ShakePosition(GameObject objectToAnimate, Vector3 shakePosition, float time, MonoBehaviour monoBehaviour = null, Action onStart = null, Action onComplete = null,
        float startDelay = 0, iTween.EaseType typeAnimation = iTween.EaseType.linear)
    {
        this.objectToAnimate = objectToAnimate;
        this.shakePosition = shakePosition;
        this.time = time;
        this.monobehaviour = monoBehaviour;
        this.onComplete = onComplete;
        this.onStart = onStart;
        this.typeAnimation = typeAnimation;
        this.startDelay = startDelay;
    }

    public async Task ExecuteAsync()
    {
        objectToAnimate.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(startDelay));

        if (onStart != null && onComplete != null) //Run when exist events to onStart and onComplete
        {
            iTween.ShakePosition(objectToAnimate, iTween.Hash("name", objectToAnimate.name, "amount", shakePosition, "islocal", true, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart != null) //Run when exist events to onStart
        {
            iTween.ShakePosition(objectToAnimate, iTween.Hash("name", objectToAnimate.name, "amount", shakePosition, "islocal", true, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart));
        }
        else if (onComplete != null) //Run when exist events to onComplete
        {
            iTween.ShakePosition(objectToAnimate, iTween.Hash("name", objectToAnimate.name, "amount", shakePosition, "islocal", true, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart == null && onComplete == null) //Run when not exist events
        {
            iTween.ShakePosition(objectToAnimate, iTween.Hash("name", objectToAnimate.name, "amount", shakePosition, "islocal", true, "easetype", typeAnimation, "time", time, "ignoretimescale", true));
        }

        await Task.Yield();
    }
}
