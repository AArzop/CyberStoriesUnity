using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeApplication : BaseApplication
{
    public Text hourText;
    
    public override void ResetApplication()
    {
    }

    // Display hour on UI
    private void SetHour()
    {
        var now = System.DateTime.Now;
        int hour = now.Hour;
        int minute = now.Minute;

        string hourStr = hour >= 10 ? hour.ToString() : "0" + hour.ToString();
        hourStr += ":";
        hourStr += minute >= 10 ? minute.ToString() : "0" + minute.ToString();

        hourText.text = hourStr;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetHour();
    }

    void Update()
    {
        SetHour();
    }

    public void PowerOff()
    {
        GlobalScreen.PowerOff();
    }

    public void LockButton()
    {

    }

    public void ChangeApplication(int index)
    {
        GlobalScreen.SwitchApplication(index);
    }
}
