using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class ComputerAsset : MonoBehaviour
{
    [FormerlySerializedAs("globalScreen")] public GlobalScreen GlobalScreen;

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None)
            GlobalScreen.PowerOn();
    }
}
