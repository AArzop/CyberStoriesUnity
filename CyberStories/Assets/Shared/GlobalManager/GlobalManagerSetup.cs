using UnityEngine;

public class GlobalManagerSetup : MonoBehaviour
{
    private void Awake()
    {
        GlobalManager.SetupManager();
    }
}