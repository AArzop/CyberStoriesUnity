using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class AnswerMenu : MonoBehaviour
{
    public SteamVR_Action_Boolean action = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("AnswerMenu");
    public GameObject menu;
    private bool visible = false;
    private Player player;

    private GameMasterManager manager;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        //menu.SetActive(false);

        manager = FindObjectOfType<GameMasterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();

        if (visible)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
        }
    }

    private void checkInput()
    {
        foreach (var hand in player.hands)
        {
            if (action.GetStateUp(hand.handType))
            {
                visible = !visible;
                return;
            }
        }
    }

    public void SendMessageToGameMaster(string message)
    {
        manager.MessageRequestToGameMaster(message);
    }
}
