using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// This class is to control pause of game
/// </summary>
public class Pause : ICommand
{
    public Pause() { }

    public async Task ExecuteAsync()
    {
        Paused();
        await Task.Yield(); 
    }

    /// <summary>
    /// Only make pause
    /// </summary>
    public void Paused()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    /// <summary>
    /// Make pause and open menu
    /// </summary>
    /// <param name="menuToOpen"> Menu to open </param>
    public void PausedAndOpenMenu(GameObject menuToOpen)
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        menuToOpen.SetActive(menuToOpen.activeSelf ? false : true);
    }
}
