using UnityEngine;

[DefaultExecutionOrder(-10000)]
public class ServicesInstaller : MonoBehaviour
{
    [SerializeField]
    private SOGameData gameData;

    private void Awake()
    {
        InstallServices();
    }

    private void InstallServices()
    {
        ServiceLocator.Instance.RegisterService(gameData); 
    }
}
