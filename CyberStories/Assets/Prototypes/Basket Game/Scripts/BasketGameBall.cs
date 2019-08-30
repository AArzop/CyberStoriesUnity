using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class BasketGameBall : MonoBehaviour
{
    private PhishingGameplayManager manager;
    public bool IsGrabbed { get; set; }

    private void Awake()
    {
        IsGrabbed = false;
        manager = FindObjectOfType<PhishingGameplayManager>();
    }

    protected void OnAttachedToHand()
    {
        IsGrabbed = true;
    }

    protected virtual void OnDetachedFromHand()
    {
        IsGrabbed = false;
    }


    private void LateUpdate()
    {
        if (transform.position.y < -5.0f)
            manager.Mark(null, this);
    }
}