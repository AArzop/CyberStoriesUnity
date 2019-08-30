using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenu : MonoBehaviour
{
    public readonly SteamVR_Action_Boolean action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("PauseMenu");
    public GameObject menu;

    private Player player;
    private Hand currentHand;

    private bool visible = false;

    private void Awake()
    {
        player = Player.instance;
        menu.SetActive(false);
    }

    private void CheckInput()
    {
        foreach (var hand in player.hands)
        {
            if (action.GetStateUp(hand.handType))
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
            menu.SetActive(true);
            menu.transform.position = currentHand.transform.position;
            menu.transform.rotation = currentHand.transform.rotation;
        }
        else
        {
            currentHand = null;
            menu.SetActive(false);
        }
    }

    public void ResumeButton()
    {
        visible = false;
        menu.SetActive(false);
    }

    public void QuitButton()
    {
        GlobalManager.QuitGameToMenu(false);
    }
}