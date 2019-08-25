using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ClickToChangeScene : MonoBehaviour
{
    public string sceneName;
    public bool lockButton = false;

    private bool isClicked = false;

    protected void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None && !isClicked && !lockButton)
        {
            isClicked = true;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void Unlock()
    {
        lockButton = false;
    }

    public void Lock()
    {
        lockButton = true;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(90, 90, 90) * Time.deltaTime);
    }
}
