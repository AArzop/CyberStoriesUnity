using System;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class BasketGameBall : MonoBehaviour
{
    private PhishingGameplayManager manager;
    public bool isGrabbed { get; set; }

    private void Awake()
    {
        isGrabbed = false;
        manager = GameObject.FindObjectOfType<PhishingGameplayManager>();
    }

    protected void OnAttachedToHand(Hand hand)
    {
        isGrabbed = true;
    }

    protected virtual void OnDetachedFromHand(Hand hand)
    {
        isGrabbed = false;
    }


    private void LateUpdate()
    {
        if (transform.position.y < 2.0f)
            manager.Mark(null, this);
    }
}
