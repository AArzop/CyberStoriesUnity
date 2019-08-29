using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class EnableGravityOnFirstUse : MonoBehaviour
{
    protected void OnAttachedToHand(Hand hand)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
    }
}