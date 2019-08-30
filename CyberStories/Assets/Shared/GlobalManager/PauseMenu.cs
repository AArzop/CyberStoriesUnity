using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenu : MonoBehaviour
{
    public readonly SteamVR_Action_Boolean Action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("PauseMenu");
    public GameObject Menu;

    private Player player;
    private Hand currentHand;

    private bool visible = false;

    private void Awake()
    {
        player = Player.instance;
        Menu.SetActive(false);
    }

    private void CheckInput()
    {
        foreach (var hand in player.hands)
        {
            if (Action.GetStateUp(hand.handType))
            {
                currentHand = hand;
                visible = !visible;
                return;
            }
        }
    }

    private void Update()
    {
        CheckInput();

        if (visible)
        {
            Menu.SetActive(true);
            Menu.transform.position = currentHand.transform.position;
            Menu.transform.rotation = currentHand.transform.rotation;
        }
        else
        {
            currentHand = null;
            Menu.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        visible = false;
        Menu.SetActive(false);
    }

    public void QuitButton()
    {
        GlobalManager.QuitGameToMenu(false);
    }
}