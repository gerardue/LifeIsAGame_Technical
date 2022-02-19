using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for scaling pingpong through commands
/// </summary>
public class ScalePingPong : ICommand
{
    //Gameobject to animate
    private GameObject objectToAnimate;

    //Target position
    private Vector3 targetScale;
    private Vector3 originalScale;

    //Time duration animation
    private float time;

    //Time delay after finished animation
    private float startDelay;

    //Type animation to use
    private iTween.EaseType typeAnimation;

    //This reference to monobehaviour is used to run events when start or completed animation
    private MonoBehaviour monoBehaviour;

    //Events
    Action onStart;
    Action onComplete;

    /// <summary>
    /// <para>
    /// Pass reference monobehaviour only if you going to use events. 
    /// To assign optional params pass them the follow way: "monoBehaviour : (your reference)" - "onStart : (your reference)" - "onComplete : (Your reference)"
    /// </para>
    /// </summary>
    public ScalePingPong(GameObject objectToAnimate, Vector3 targetScale, float time, MonoBehaviour monoBehaviour = null, Action onStart = null, Action onComplete = null,
        float startDelay = 0, iTween.EaseType typeAnimation = iTween.EaseType.linear)
    {
        this.objectToAnimate = objectToAnimate;
        this.targetScale = targetScale;
        this.time = time;
        this.monoBehaviour = monoBehaviour;
        this.onComplete = onComplete;
        this.onStart = onStart;
        this.typeAnimation = typeAnimation;
        this.startDelay = startDelay;
        originalScale = objectToAnimate.transform.localScale;
    }

    //Run
    public async Task ExecuteAsync()
    {
        objectToAnimate.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(startDelay));

        iTween.ScaleTo(objectToAnimate, iTween.Hash("scale", targetScale, "easetype", typeAnimation, "time", time / 2, "ignoretimescale", true));

        await Task.Delay(TimeSpan.FromSeconds(time / 2));

        if (onStart != null && onComplete != null) //Run when exist events to onStart and onComplete
        {
            iTween.ScaleTo(objectToAnimate, iTween.Hash("scale", originalScale, "name", objectToAnimate.name, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monoBehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart,
                "oncompletetarget", monoBehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart != null) //Run when exist events to onStart
        {
            iTween.ScaleTo(objectToAnimate, iTween.Hash("scale", originalScale, "name", objectToAnimate.name, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monoBehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart));
        }
        else if (onComplete != null) //Run when exist events to onComplete
        {
            iTween.ScaleTo(objectToAnimate, iTween.Hash("scale", originalScale, "name", objectToAnimate.name, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "oncompletetarget", monoBehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete));
        }
        else if (onStart == null && onComplete == null) //Run when not exist events
        {
            iTween.ScaleTo(objectToAnimate, iTween.Hash("scale", originalScale, "name", objectToAnimate.name, "easetype", typeAnimation, "time", time, "ignoretimescale", true));
        }

        await Task.Yield();
    }
}
