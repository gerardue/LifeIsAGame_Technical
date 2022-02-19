using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum TypeMoveSlider
{
    ContinueAutomatic,
    DiscreteAutomatic,
    CustomNoAutomatic
}

/// <summary>
/// This class is responsible for moving slider through commands
/// </summary>
public class MoveSlider : ICommand
{
    //Gameobject to animate
    private Image subject;

    //Values
    private float initialValue;
    private float targetValue;
    private float increaseIn;

    //Time duration animation
    private float time;

    //Times Delay
    private float startDelay;
    private float stepTime;

    //Type
    private bool isDecreasing;

    //Type Slider
    private TypeMoveSlider typeMoveSlider;

    //Events
    private Action onStart;
    private Action onComplete;
    private Action onUpdate;

    /// <summary>
    /// <para>
    /// INSTRUCTIONS
    /// Call optional parameters in a follow way: "stepsToComplete : 6", "increaseIn : 0.1f" \n
    /// To Custom No Automatic use the following optional parameters : "initialValue", "targetValue", "increaseIn"/n
    /// To ContinueAutomatic use the following optinal parameters : "initialValue", "targetValue", "time"
    /// To DiscreteAutomatic use the following optinal parameters : "initialValue", "targetValue", "time", "moveEachSpecifyTime"
    /// </para>
    /// </summary>
    public MoveSlider(Image objectToAnimate, TypeMoveSlider typeMoveSlider, float moveEachSpecifyTime = 0, float increaseIn = 0, float initialValue = 0,
        float targetValue = 0, float time = 0, float startDelay = 0, Action onStart = null, Action onComplete = null, Action onUpdate = null)
    {
        this.subject = objectToAnimate;
        this.initialValue = initialValue;
        this.targetValue = targetValue;
        this.time = time;
        this.startDelay = startDelay;
        this.onStart = onStart;
        this.onComplete = onComplete;
        this.onUpdate = onUpdate;
        isDecreasing = IsDecreasing(initialValue, targetValue);
        this.typeMoveSlider = typeMoveSlider;
        if (moveEachSpecifyTime == 0)
            this.increaseIn = increaseIn;
        else
        {
            this.increaseIn = moveEachSpecifyTime;
        }
        this.stepTime = moveEachSpecifyTime;
        if (typeMoveSlider != TypeMoveSlider.CustomNoAutomatic)
            subject.fillAmount = initialValue;
    }

    //Run
    public async Task ExecuteAsync()
    {
        if (startDelay >= 0) await Task.Delay(TimeSpan.FromSeconds(startDelay)); //With start delay

        if (typeMoveSlider == TypeMoveSlider.CustomNoAutomatic) CoreTraditional().WrapErrors();
        else if (typeMoveSlider == TypeMoveSlider.ContinueAutomatic) CoreContinueAutomatic().WrapErrors();
        else if (typeMoveSlider == TypeMoveSlider.DiscreteAutomatic) CoreDiscreteAutomatic().WrapErrors();

        await Task.Yield();
    }

    async Task CoreTraditional()
    {
        if (subject.fillAmount == initialValue) onStart?.Invoke();

        onUpdate?.Invoke();

        if (isDecreasing)
            UpdateSlider(-increaseIn);
        else
            UpdateSlider(increaseIn);

        //Await until complete slider to run onComplete
        if (isDecreasing)
        {
            if (subject.fillAmount <= targetValue) onComplete?.Invoke();
        }
        else
        {
            if (subject.fillAmount >= targetValue) onComplete?.Invoke();
        }

        await Task.Yield();
    }

    async Task CoreContinueAutomatic()
    {
        //Run onStart
        onStart?.Invoke();

        float elapsedTime = 0;
        bool isRunning = true;
        while (isRunning)
        {
            onUpdate?.Invoke();
            elapsedTime += Time.deltaTime;
            float fill;
            if (isDecreasing)
                fill = 1 * Mathf.Clamp01(1 - (elapsedTime / time));
            else
                fill = 1 * Mathf.Clamp01((elapsedTime / time));

            UpdateSlider(fill);

            if (isDecreasing)
            {
                if (fill <= targetValue) isRunning = false;
            }
            else
            {
                if (fill >= targetValue) isRunning = false;
            }

            await Task.Yield();
        }

        //Run onComplete
        onComplete?.Invoke();
    }

    async Task CoreDiscreteAutomatic()
    {
        onStart?.Invoke();

        float elapsedTime = 0;
        bool isRunning = true;
        float step = stepTime;
        while (isRunning)
        {
            onUpdate?.Invoke();
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= step)
            {
                float fillValue = 0;
                if (isDecreasing)
                    fillValue = 1 * Mathf.Clamp01(1 - (step / time));
                else
                    fillValue = 1 * Mathf.Clamp01((step / time));

                UpdateSlider(fillValue);
                step += stepTime;
            }

            if (elapsedTime >= time) isRunning = false;

            await Task.Yield();
        }

        //Run onComplete
        onComplete?.Invoke();
    }

    private void UpdateSlider(float value)
    {
        if (typeMoveSlider == TypeMoveSlider.ContinueAutomatic || typeMoveSlider == TypeMoveSlider.DiscreteAutomatic)
            subject.fillAmount = value;
        else if (typeMoveSlider == TypeMoveSlider.CustomNoAutomatic)
            subject.fillAmount += value;
    }

    private bool IsDecreasing(float initValue, float finishValue)
    {
        if (initialValue < finishValue)
            return false;
        else
            return true;
    }
}
