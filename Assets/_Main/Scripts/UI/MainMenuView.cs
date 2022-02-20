using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for the UI from Home Scene
/// </summary>
public class MainMenuView : MonoBehaviour
{
    [Header("Dances")]
    [SerializeField]
    private PlayerDancing danceHouse;
    [SerializeField]
    private PlayerDancing danceMacarena;
    [SerializeField]
    private PlayerDancing danceHipHop;

    [Header("UI elements")]
    [SerializeField]
    private GameObject buttonStart;
    [SerializeField]
    private GameObject danceHouseUI;
    [SerializeField]
    private GameObject danceMacarenaUI;
    [SerializeField]
    private GameObject danceHipHopUI;

    #region Unity Messages

    private void Start()
    {
        danceHipHop.OnSelectDance(OnSelectDance);
        danceHouse.OnSelectDance(OnSelectDance);
        danceMacarena.OnSelectDance(OnSelectDance);
        AnimationWireframeDances();
    }

    #endregion

    #region Public Methods

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    #endregion

    #region Private Methods

    private void OnSelectDance(string nameDance)
    {
        ServiceLocator.Instance.GetService<SOGameData>().NameAnimation = nameDance;
        AnimationStartButton();
    }

    private void AnimationStartButton()
    {
        buttonStart.SetActive(true);
        ICommand command = new ScalePingPong(buttonStart, Vector3.one * 1.5f, 0.5f);
        CommandQueue.Instance.ExecuteCommand(command);
    }

    private void AnimationWireframeDances()
    {
        danceHipHopUI.SetActive(true);
        danceMacarenaUI.SetActive(true);
        danceHouseUI.SetActive(true);

        List<ICommand> commands = new List<ICommand>()
        {
            new Scale(danceHipHopUI, Vector3.one, 0.3f),
            new Scale(danceMacarenaUI, Vector3.one, 0.3f),
            new Scale(danceHouseUI, Vector3.one, 0.3f),
        };

        CommandQueue.Instance.ExecuteCommands(commands);
    }

    #endregion
}
