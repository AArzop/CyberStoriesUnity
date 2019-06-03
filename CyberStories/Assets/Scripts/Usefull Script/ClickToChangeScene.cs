using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ClickToChangeScene : MonoBehaviour
{
    public string sceneName;

    private bool isClicked = false;

    protected void OnAttachedToHand(Hand hand)
    {
        if (!isClicked)
        {
            isClicked = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}
