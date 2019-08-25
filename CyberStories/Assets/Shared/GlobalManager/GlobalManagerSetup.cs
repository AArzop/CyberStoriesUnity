using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManagerSetup : MonoBehaviour
{
    private void Awake()
    {
        GlobalManager.SetupManager();
    }
}
