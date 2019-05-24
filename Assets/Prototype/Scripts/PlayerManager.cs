using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Tooltip("When debugging, you can add multiple players to one controller.")]
    public bool debug;
    [Tooltip("Should players be able to join?")]
    public bool acceptNewPlayers;
    List<Vector3> playerPositions = new List<Vector3>() {
            new Vector3( -3, 0.5f, 0 ),
            new Vector3( -1, 0.5f, 0 ),
            new Vector3( 1, 0.5f, 0 ),
            new Vector3( 3, 0.5f, 0 ),
        };

    public GameObject[] playerPrefabs;
    public delegate void PlayerListHandler(Player player);
    public event PlayerListHandler OnPlayerAdded;
    public event PlayerListHandler OnPlayerRemoved;

    const int maxPlayers = 4;
    public List<Player> players = new List<Player>(maxPlayers);

    public PlayerActions keyboardListener;
    public PlayerActions joystickListener;
    PlayerBindings bindings;

    void OnEnable()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InputManager.OnDeviceDetached += OnDeviceDetached;
        bindings = GetComponent<PlayerBindings>();
        keyboardListener = PlayerActions.CreateWithKeyboardBindings(bindings);
        joystickListener = PlayerActions.CreateWithJoystickBindings(bindings);
    }


    void OnDisable()
    {
        InputManager.OnDeviceDetached -= OnDeviceDetached;
        joystickListener.Destroy();
        keyboardListener.Destroy();
    }

    void Update()
    {
        if (JoinButtonWasPressedOnListener(joystickListener)) // This will fire no matter which device does the pressing.
        {
            var inputDevice = InputManager.ActiveDevice; // ActiveDevice is the device that just pressed a button.

            if (debug || ThereIsNoPlayerUsingJoystick(inputDevice)) // Each player has a Device, and this method loops through all current players and checks if inputDevice appears in any of the players.
            {
                CreatePlayer(inputDevice); //...Then if there isn't, create a player from prefab and add inputDevice as the player's device.
            }
        }

        if (JoinButtonWasPressedOnListener(keyboardListener))
        {
            if (debug || ThereIsNoPlayerUsingKeyboard())
            {
                CreatePlayer(null);
            }
        }
    }

    bool JoinButtonWasPressedOnListener(PlayerActions actions)
    {
        return actions.Submit.WasPressed;
    }


    Player FindPlayerUsingJoystick(InputDevice inputDevice)
    {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.Actions.Device == inputDevice)
            {
                return player;
            }
        }

        return null;
    }


    bool ThereIsNoPlayerUsingJoystick(InputDevice inputDevice)
    {
        return FindPlayerUsingJoystick(inputDevice) == null;
    }


    Player FindPlayerUsingKeyboard()
    {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.Actions == keyboardListener)
            {
                return player;
            }
        }

        return null;
    }


    bool ThereIsNoPlayerUsingKeyboard()
    {
        return FindPlayerUsingKeyboard() == null;
    }

    void OnDeviceDetached(InputDevice inputDevice)
    {
        var player = FindPlayerUsingJoystick(inputDevice);
        if (player != null)
        {
            RemovePlayer(player);
        }
    }

    Player CreatePlayer(InputDevice inputDevice)
    {
        if (players.Count < maxPlayers && acceptNewPlayers)
        {
            var playerPosition = playerPositions[0]; // Pop off a player spawn position in the lobby. To fix later.
            playerPositions.RemoveAt(0);

            var gameObject = (GameObject)Instantiate(playerPrefabs[players.Count], playerPosition, Quaternion.identity); // Change this, asshole. Spawning a player in the middle of  the camera is a terrible idea. Go jump into a volcano.
            var player = gameObject.GetComponent<Player>();

            if (inputDevice == null)
            {
                // We could create a new instance, but might as well reuse the one we have
                // and it lets us easily find the keyboard player.
                player.Actions = keyboardListener; // If we want different bindings for each player, this needs to be a new instance.
            }
            else
            {
                // Create a new instance and specifically set it to listen to the
                // given input device (joystick).
                var actions = PlayerActions.CreateWithJoystickBindings(bindings);
                actions.Device = inputDevice;

                player.Actions = actions; // The player will listen for changes to its Actions, so this is what we're setting here.
            }

            if (!players.Contains(player))
            {
                players.Add(player); // The list of players in the game. We'll have to make sure that list list of players is the one on the GameManager.
                if (OnPlayerAdded != null)
                {
                    OnPlayerAdded(player);
                }
            }
            DontDestroyOnLoad(player.gameObject);
            
            return player;
        }

        return null;
    }


    public void RemovePlayer(Player player)
    {
        playerPositions.Insert(0, player.transform.position);
        players.Remove(player);
        player.Actions = null;
        if (OnPlayerRemoved != null)
        {
            OnPlayerRemoved(player);
        }
    }
}
