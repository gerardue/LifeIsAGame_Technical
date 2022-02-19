using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum TypeFade
{
    FadeIn, 
    FadeOut
}

/// <summary>
/// This class is responsable for run effect "FadeOut"
/// </summary>
public class FadeOut : ICommand
{
    //Object to fade
    private GameObject objectFade;

    //Canvas group
    private CanvasGroup canvasFade;

    //Duration fade
    private float fadeDuration;

    //Delay to start
    private float startDelay;

    //Type of fade
    private TypeFade typeFade; 

    //Events
    private Action onStart;
    private Action onComplete;

    public FadeOut()
    {

    }

    #region Methods with commands

    /// <summary>  
    /// <para> 
    /// Pass reference monobehaviour only if you going to use events.
    /// To assign optional params pass them the follow way: "monoBehaviour : (your reference)" - "onStart : (your reference)" - "onComplete : (Your reference)" 
    /// </para>
    /// </summary>
    public FadeOut(GameObject objectFade, float fadeDuration, TypeFade typeFade, float startDelay = 0, Action onStart = null, Action onComplete = null)
    {
        this.objectFade = objectFade;
        GetCanvasGroup();
        this.fadeDuration = fadeDuration;
        this.startDelay = startDelay;
        this.typeFade = typeFade;
        this.onStart = onStart;
        this.onComplete = onComplete;
    }

    //Get canvas group or to create it 
    private void GetCanvasGroup()
    {
        if (objectFade.GetComponent<CanvasGroup>() == null) objectFade.AddComponent<CanvasGroup>();
        canvasFade = objectFade.GetComponent<CanvasGroup>();
    }

    //Run
    public async Task ExecuteAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(startDelay));
        onStart?.Invoke();

        await RunFade();

        onComplete?.Invoke();
    } 

    //Logic to fade
    public async Task RunFade()
    {
        objectFade.SetActive(true);
        if (typeFade == TypeFade.FadeIn)
            canvasFade.alpha = 0;
        else if (typeFade == TypeFade.FadeOut)
            canvasFade.alpha = 1; 

        float elapsedTime = 0;
        bool isFadeOut = true;
        while (isFadeOut)
        {
            elapsedTime += Time.deltaTime;
            float alfa = 0;
            if(typeFade == TypeFade.FadeIn)
            {
                alfa = Mathf.Clamp01((elapsedTime / fadeDuration));
                if (alfa == 1) isFadeOut = false;
            }
            else if(typeFade == TypeFade.FadeOut)
            {
                alfa = 1 * Mathf.Clamp01(1 - (elapsedTime / fadeDuration));
                if (alfa == 0) isFadeOut = false;
            }
            canvasFade.alpha = alfa;
            if (alfa == 0) isFadeOut = false;
            await Task.Yield();
        }
    }

    #endregion


    #region Methods to run indepently

    /// <summary>
    /// Run Effect FadeOut
    /// </summary>
    public async Task RunFadeOutScreen(float timeFadeOut, Image background)
    {
        bool isFadeOut = false;
        background.color = new Color(background.color.r, background.color.g, background.color.b, 1);
        background.gameObject.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.7f));

        float elapsedTime = 0;
        isFadeOut = true;
        while (isFadeOut)
        {
            elapsedTime += Time.deltaTime;
            float alfa = 1 * Mathf.Clamp01(1 - (elapsedTime / timeFadeOut));
            background.color = new Color(background.color.r, background.color.g, background.color.b, alfa);
            if (alfa == 0) isFadeOut = false;
            await Task.Yield();
        }
        background.gameObject.SetActive(false);
    }

    /// <summary>
    /// Run Effect FadeOut
    /// </summary>
    public async Task RunFadeOutScreen(float timeFadeOut, float delayTime, Image background, System.Action onStart = null, System.Action onFinished = null)
    {
        background.color = new Color(background.color.r, background.color.g, background.color.b, 1);
        background.gameObject.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(delayTime));
        onStart?.Invoke(); 

        float elapsedTime = 0;
        bool isFadeOut = true;
        while (isFadeOut)
        {
            elapsedTime += Time.deltaTime;
            float alfa = 1 * Mathf.Clamp01(1 - (elapsedTime / timeFadeOut));
            background.color = new Color(background.color.r, background.color.g, background.color.b, alfa);
            if (alfa == 0) isFadeOut = false;
            await Task.Yield();
        }
        background.gameObject.SetActive(false);
        onFinished?.Invoke(); 
    }

    /// <summary>
    /// Run Effect FadeOut and change color of fadeOut
    /// </summary>
    public async Task RunFadeOutScreen(float timeFadeOut, Image background, Color color)
    {
        background.color = color;
        background.gameObject.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.2f));

        float elapsedTime = 0;
        bool isFadeOut = true;
        while (isFadeOut)
        {
            elapsedTime += Time.deltaTime;
            float alfa = 1 * Mathf.Clamp01(1 - (elapsedTime / timeFadeOut));
            background.color = new Color(background.color.r, background.color.g, background.color.b, alfa);
            if (alfa == 0) isFadeOut = false;
            await Task.Yield();
        }
        background.gameObject.SetActive(false);
    } 

    #endregion
}