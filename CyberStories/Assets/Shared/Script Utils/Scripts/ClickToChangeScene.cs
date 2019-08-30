using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ClickToChangeScene : MonoBehaviour
{
    public string SceneName;
    public bool LockButton = false;

    private bool isClicked = false;

    protected void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None && !isClicked && !LockButton)
        {
            isClicked = true;
            SceneManager.LoadScene(SceneName);
        }
    }

    public void Unlock()
    {
        LockButton = false;
    }

    public void Lock()
    {
        LockButton = true;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(90, 90, 90) * Time.deltaTime);
    }
}
