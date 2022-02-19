using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for moving through commands
/// </summary>
public class Move : ICommand
{
    //Gameobject to animate
    private GameObject objectToAnimate;

    //Target position
    private Vector3 targetPosition;

    //Local Position
    private bool isLocal;

    //Time duration animation
    private float time;

    //Time delay after finished animation
    private float startDelay; 

    //Type animation to use
    private iTween.EaseType typeAnimation;

    //Loop type
    private iTween.LoopType loop; 

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
    public Move(GameObject objectToAnimate, Vector3 targetPosition, float time, MonoBehaviour monoBehaviour = null, Action onStart = null, Action onComplete = null,
        float startDelay = 0, iTween.EaseType typeAnimation = iTween.EaseType.linear,  iTween.LoopType loop = iTween.LoopType.none, bool isLocal = false)
    {
        this.objectToAnimate = objectToAnimate;
        this.targetPosition = targetPosition;
        this.time = time;
        this.monobehaviour = monoBehaviour;
        this.onComplete = onComplete;
        this.onStart = onStart;
        this.typeAnimation = typeAnimation;
        this.loop = loop; 
        this.startDelay = startDelay;
        this.isLocal = isLocal; 
    }

    //Run
    public async Task ExecuteAsync()
    {
        objectToAnimate.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(startDelay)); 
        if(onStart != null && onComplete != null) //Run when exist events to onStart and onComplete
        {
            iTween.MoveTo(objectToAnimate, iTween.Hash("position", targetPosition, "name", objectToAnimate.name, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete,
                "looptype", loop));
        }
        else if (onStart != null) //Run when exist events to onStart
        {
            iTween.MoveTo(objectToAnimate, iTween.Hash("position", targetPosition, "name", objectToAnimate.name, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "onstarttarget", monobehaviour.gameObject, "onstart", "OnStartCommandUI", "onstartparams", onStart,
                "looptype", loop));
        }
        else if(onComplete != null) //Run when exist events to onComplete
        {
            iTween.MoveTo(objectToAnimate, iTween.Hash("position", targetPosition, "name", objectToAnimate.name, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "oncompletetarget", monobehaviour.gameObject, "oncomplete", "OnCompleteCommandUI", "oncompleteparams", onComplete,
                "looptype", loop));
        }
        else if(onStart == null && onComplete == null) //Run when not exist events
        {
            iTween.MoveTo(objectToAnimate, iTween.Hash("position", targetPosition, "name", objectToAnimate.name, "islocal", isLocal, "easetype", typeAnimation, "time", time, "ignoretimescale", true,
                "looptype", loop));
        }

        await Task.Yield();
    }
}
