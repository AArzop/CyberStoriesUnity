using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Clock : MonoBehaviour
{
    private const int MaxSeconds = 60;
    private const int MaxMinutes = 60;
    private const int MaxHours = 12;

    // set start time 00:00
    [FormerlySerializedAs("minutes")] public int Minutes = 0;

    [FormerlySerializedAs("hour")] public int Hour = 0;

    // If true, take hour, minutes and seconds will be updated according to DateTime.Now
    [FormerlySerializedAs("isLocalTime")] public bool IsLocalTime;

    // time speed factor
    [FormerlySerializedAs("clockSpeed")]
    public float ClockSpeed = 1.0f; // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    // private vars
    private int seconds;
    private float msecs;

    // private game object
    private GameObject pointerSeconds;
    private GameObject pointerMinutes;
    private GameObject pointerHours;

    private void Start()
    {
        pointerSeconds = transform.Find("rotation_axis_pointer_seconds").gameObject;
        pointerMinutes = transform.Find("rotation_axis_pointer_minutes").gameObject;
        pointerHours = transform.Find("rotation_axis_pointer_hour").gameObject;

        msecs = 0.0f;
        seconds = 0;

        if (IsLocalTime)
        {
            DateTime currentTime = DateTime.Now;
            Hour = currentTime.Hour;
            Minutes = currentTime.Minute;
            seconds = currentTime.Second;
        }
    }

    private void Update()
    {
        // calculate time
        msecs += Time.deltaTime * ClockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            if (++seconds >= MaxSeconds)
            {
                seconds = 0;
                if (++Minutes > MaxMinutes)
                {
                    Minutes = 0;
                    if (++Hour >= 2 * MaxHours)
                        Hour = 0;
                }
            }
        }


        // calculate pointer angles
        const float MaxAngle = 360.0f;
        float rotationSeconds = (MaxAngle / Convert.ToSingle(MaxSeconds)) * seconds;
        float rotationMinutes = (MaxAngle / Convert.ToSingle(MaxMinutes)) * Minutes;
        float rotationHours = ((MaxAngle / Convert.ToSingle(MaxHours)) * Hour) +
                              ((MaxAngle / (Convert.ToSingle(MaxMinutes) * Convert.ToSingle(MaxHours))) * Minutes);

        // draw pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
    }
}