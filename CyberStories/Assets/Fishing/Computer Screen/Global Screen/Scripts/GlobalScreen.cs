using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalScreen : MonoBehaviour
{
    [FormerlySerializedAs("applications")] public List<BaseApplication> Applications;
    private BaseApplication currentApplication;
    private int currentIndex;

    // Start is called before the first frame update
    private void Start()
    {
        currentIndex = 0;
        currentApplication = Applications[currentIndex];
    }

    // Turn the screen on, show and go to Password/Home Application
    public void PowerOn()
    {
        if (gameObject.activeSelf)
            return;

        gameObject.SetActive(true);
        currentIndex = -1; // Force switch
        BackHomeButton();
    }

    // Turn the screen off, hide and reset component
    public void PowerOff()
    {
        gameObject.SetActive(false);
    }

    // Reset the current application and switch to Home Application
    public void BackHomeButton()
    {
        SwitchApplication(0);
    }

    // Switch current application
    public void SwitchApplication(int index)
    {
        if (index == currentIndex)
            return;

        BaseApplication app = Applications[index];

        currentApplication?.ResetApplication();
        currentApplication?.gameObject.SetActive(false);

        app.gameObject.SetActive(true);
        app.ResetApplication();
        currentApplication = app;
        currentIndex = index;
    }

    public void LinkClicked(string url)
    {
        SwitchApplication(1);
        WebApplication webApplication = currentApplication as WebApplication;
        if (webApplication != null)
        {
            webApplication.Redirect(url);
        }
    }
}
