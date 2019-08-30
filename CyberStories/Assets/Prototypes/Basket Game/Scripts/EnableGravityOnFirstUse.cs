using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class EnableGravityOnFirstUse : MonoBehaviour
{
    protected void OnAttachedToHand()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
    }
}