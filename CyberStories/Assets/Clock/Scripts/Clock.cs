using System;
using UnityEngine;

public class Clock : MonoBehaviour {
    private static int MaxSeconds = 60;
    private static int MaxMinutes = 60;
    private static int MaxHours = 12;

    // set start time 00:00
    public int minutes = 0;
    public int hour = 0;
    // If true, take hour, minutes and seconds will be updated according to DateTime.Now
    public bool isLocalTime;
    // time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    // private vars
    private int seconds;
    private float msecs;

    // private gameobject
    private GameObject pointerSeconds;
    private GameObject pointerMinutes;
    private GameObject pointerHours;

    void Start() 
    {
        pointerSeconds = transform.Find("rotation_axis_pointer_seconds").gameObject;
        pointerMinutes = transform.Find("rotation_axis_pointer_minutes").gameObject;
        pointerHours   = transform.Find("rotation_axis_pointer_hour").gameObject;

        msecs = 0.0f;
        seconds = 0;

        if (isLocalTime)
        {
            DateTime currentTime = DateTime.Now;
            hour = currentTime.Hour;
            minutes = currentTime.Minute;
            seconds = currentTime.Second;
        }
    }

    void Update() 
    {
        // calculate time
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            if (++seconds >= MaxSeconds)
            {
                seconds = 0;
                if (++minutes > MaxMinutes)
                {
                    minutes = 0;
                    if (++hour >= 2 * MaxHours)
                        hour = 0;
                }
            }
        }


        // calculate pointer angles
        const float maxAngle = 360.0f;
        float rotationSeconds = (maxAngle / Convert.ToSingle(MaxSeconds)) * seconds;
        float rotationMinutes = (maxAngle / Convert.ToSingle(MaxMinutes)) * minutes;
        float rotationHours   = ((maxAngle / Convert.ToSingle(MaxHours)) * hour) +
                                ((maxAngle / (Convert.ToSingle(MaxMinutes) * Convert.ToSingle(MaxHours))) * minutes);

        // draw pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles   = new Vector3(0.0f, 0.0f, rotationHours);
    }
}