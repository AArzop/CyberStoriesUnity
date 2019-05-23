using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class BasketGameBall : MonoBehaviour
{
    public DateTime launchTime { get; set; }
    public Vector3 launchPosition { get; set; }
    public bool isGrabbed { get; set; }

    private void Awake()
    {
        launchPosition = Vector3.zero;
        launchTime = DateTime.Now;
        isGrabbed = false;
    }

    protected void OnAttachedToHand(Hand hand)
    {
        isGrabbed = true;
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        isGrabbed = false;
        launchPosition = gameObject.transform.position;
    }
}
