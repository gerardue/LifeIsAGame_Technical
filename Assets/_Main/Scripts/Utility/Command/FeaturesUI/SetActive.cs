using System;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is responsible for active game object through commands
/// </summary>
public class SetActive : ICommand
{
    private GameObject frameUI;
    private bool setActiveObject;
    private float timeDelay; 

    public SetActive(GameObject frameUI, bool setActiveObject, float timeDelay = 0)
    {
        this.frameUI = frameUI;
        this.setActiveObject = setActiveObject;
        this.timeDelay = timeDelay; 
    }
     
    public async Task ExecuteAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(timeDelay)); 
        frameUI.SetActive(setActiveObject);
        await Task.Yield();
    }
}