using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public PlayerManager playerManager;
    PlayerActions keyboardActions;
    PlayerActions joystickActions;
    AudioHandler audioHandler;

    private void OnEnable()
    {
        PlayerManager.instance.OnPlayerAdded += OnPlayerJoin;
    }

    void OnDisable()
    {
        PlayerManager.instance.OnPlayerAdded -= OnPlayerJoin;
    }

    void Start()
    {
        playerManager = GameManager.instance.playerManager;
        keyboardActions = playerManager.joystickListener;
        joystickActions = playerManager.keyboardListener;
        playerManager.RemoveAllPlayers();
        playerManager.acceptNewPlayers = true;
        audioHandler = GetComponent<AudioHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (commandButtonWasPressed())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_master");
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

    void OnPlayerJoin(Player player)
    {
        audioHandler.PlayOneShotWithRandomPitch("Join");
    }
}
