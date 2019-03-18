using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScreen : MonoBehaviour
{
    public List<BaseApplication> applications;
    public BaseApplication currentApplication;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Turn the screen on, show and go to Password/Home Application
    public void PowerOn()
    {
        gameObject.SetActive(true);
    }

    // Turn the screen off, hide and reset component
    public void PowerOff()
    {
        this.gameObject.SetActive(false);
    }

    // Reset the current application and switch to Home Application
    public void BackHomeButton()
    {

    }

    // Switch current application to app
    private void SwitchApplication(BaseApplication app)
    {
        currentApplication.ResetApplication();
        currentApplication.gameObject.SetActive(false);

        app.ResetApplication();
        app.gameObject.SetActive(true);
        currentApplication = app;
    }
}
