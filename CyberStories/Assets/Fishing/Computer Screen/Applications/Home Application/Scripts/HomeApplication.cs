using System;
using UnityEngine.UI;

public class HomeApplication : BaseApplication
{
    public Text HourText;

    public override void ResetApplication()
    {
    }

    // Display hour on UI
    private void SetHour()
    {
        DateTime now = DateTime.Now;
        int hour = now.Hour;
        int minute = now.Minute;

        string hourStr = hour >= 10 ? hour.ToString() : "0" + hour;
        hourStr += ":";
        hourStr += minute >= 10 ? minute.ToString() : "0" + minute;

        HourText.text = hourStr;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetHour();
    }

    private void Update()
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