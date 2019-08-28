using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ComputerAsset : MonoBehaviour
{
    [FormerlySerializedAs("GlobalScreen")] public GlobalScreen globalScreen;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
            globalScreen.PowerOn();
    }
}
