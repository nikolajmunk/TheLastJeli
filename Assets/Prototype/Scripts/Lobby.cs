﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public PlayerManager playerManager;
    PlayerActions keyboardActions;
    PlayerActions joystickActions;
    void Start()
    {
        keyboardActions = playerManager.joystickListener;
        joystickActions = playerManager.keyboardListener;
    }

    // Update is called once per frame
    void Update()
    {
        if (commandButtonWasPressed())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            foreach (Player player in PlayerManager.instance.players)
            {
                //player.gameObject.SetActive(false);
            }
        }
    }

    bool commandButtonWasPressed()
    {
        return keyboardActions.Command.WasPressed || joystickActions.Command.WasPressed;
    }

}
