using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionCommandUI 
{
    public void OnStartCommandUI(System.Action onAction); 
    public void OnCompleteCommandUI(System.Action onAction);
}
