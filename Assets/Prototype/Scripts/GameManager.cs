﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Declarations")]
    public GameObject teleportEffect;
    public PlayerManager playerManager;
    public LevelGenerator levelGenerator;
    [HideInInspector]
    public GameObject spaceShipModule;
    public GameObject destructionZone;
    public GameObject reincarnationPrefab;
    [Header("Player information")]
    [HideInInspector]
    public GameObject frontPlayer;
    public List<Player> activePlayers;
    public List<Transform> playerPositions;
    public int numberOfActivePlayers;

    public bool debug;
    [HideInInspector]
    public Vector3 killBoxPosition;

    public bool isEndGame = false;
    public bool isEveryoneDead = false;
    public bool isGameOver = false;
    public bool onePlayer = false;
    [HideInInspector]
    public bool playerInSpaceship = false;

    public delegate void GameStateEvent();
    public GameStateEvent OnWin;
    public GameStateEvent OnAllPlayersDead;
    public GameStateEvent OnEndGame;
    public GameStateEvent OnReadyToPlay;

    public bool isPaused = false;

    [HideInInspector]
    public float twoPlayerSpaceshipSpawnTime;
    [HideInInspector]
    public bool hasGameStarted = false;

    public delegate void PauseState();
    public PauseState OnPause;
    public PauseState OnUnpause;

    private void OnEnable()
    {
        playerManager.OnPlayerAdded += OnPlayerAdded;
        playerManager.OnPlayerRemoved += OnPlayerRemoved;
    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            playerManager = GetComponent<PlayerManager>();
            playerManager.enabled = true;
        }
        else
        {
            Destroy(gameObject);
        }

        if (playerManager == null)
        {
            if (PlayerManager.instance != null)
            {
                playerManager = PlayerManager.instance;
            }
            else
            {
                playerManager = GetComponent<PlayerManager>();
                playerManager.enabled = true;
            }
        }

        killBoxPosition = Vector3.zero;
    }

    void Update()
    {

        playerPositions.Sort(SortByXPosition);
        playerPositions.Reverse();
        if (playerPositions.Count != 0)
        {
            frontPlayer = playerPositions[0].gameObject;
        }

        if (numberOfActivePlayers == 1 && onePlayer == false && hasGameStarted)
        {
            OnePlayerLeft();
        }

        // Only two players left
        if (numberOfActivePlayers == 2 && isEndGame == false && SceneManager.GetActiveScene().name == "Scene_master" && hasGameStarted)
        {
            EndGame();
        }

        // If one player reaches the spaceship, she wins
        if (playerInSpaceship == true && isGameOver == false)
        {
            Win(5);
        }

        // If all players die
        if (numberOfActivePlayers == 0 && isEndGame == true && isEveryoneDead == false)
        {
            // Do stuff here for when everyone is out of the game

            OnAllPlayersDead();
            isEveryoneDead = true;
        }

        if((PlayerManager.instance.joystickListener.Command || PlayerManager.instance.keyboardListener.Command) && hasGameStarted) {
            if (!isPaused) {
                OnPause();
            }
            else {
                OnUnpause();
            }
        }
    }

    public bool CanBeTeleported(GameObject actor)
    {
        Teleportable teleportable = actor.GetComponent<Teleportable>();
        if (teleportable != null)
        {
            return teleportable.canBeTeleported;
        }
        return false;
    }

    public void EndGame()
    {
        StartCoroutine(WaitToSpawnShip(twoPlayerSpaceshipSpawnTime));
        OnEndGame();
        isEndGame = true;
    }

    public void OnePlayerLeft()
    {
        levelGenerator.SpawnChunk(spaceShipModule, levelGenerator.GetPoint(levelGenerator.mostRecentModule, "ExitPoint").position);
        levelGenerator.generateLevels = false;
        onePlayer = true;
    }

    public void Win(float delay)
    {
        if (!debug)
        {
            destructionZone.GetComponent<DestructionZone>().move = false;
            //          buttonScript.restartB.SetActive(true);
            //          buttonScript.endB.SetActive(true);
            //winUI.SetActive(true);
            //winText.text = activePlayers[0].playerName + " wins! \n \n No cows were hurt in the destruction of this planet";
            //StartCoroutine(Restart(delay)); // Removing this for now; Restarting doesn't work well. NMBJ
            OnWin();
            isGameOver = true;
        }
    }

    public void AllPlayersDead()
    {
        
        OnAllPlayersDead();
    }

    void OnPlayerAdded(Player player)
    {
        activePlayers.Add(player);
        numberOfActivePlayers = activePlayers.Count;
        playerPositions.Add(player.transform);
    }

    void OnPlayerRemoved(Player player)
    {
        activePlayers.Remove(player);
        numberOfActivePlayers = activePlayers.Count;
        playerPositions.Remove(player.transform);
    }

    public void KillPlayer(Player player)
    {
        var effect = Instantiate(player.deathEffect,Camera.main.ViewportToWorldPoint(GetIntersectionWithScreen(player.transform)), Quaternion.identity);
        Debug.Log(player.transform.position + " " + effect.transform.position);
        OnPlayerRemoved(player);

        player.GetComponent<AudioHandler>().PlayOneShotByName("Death");

        //StartCoroutine(KillTime(player.gameObject));
        player.gameObject.SetActive(false);
        Reincarnate(player);
    }

    Vector2 GetIntersectionWithScreen(Transform target)
    {
        var screenWidth = Camera.main.rect.width;
        var screenHeight = Camera.main.rect.height;
        Vector2 vector =  Camera.main.rect.center - (Vector2)Camera.main.WorldToViewportPoint(target.position);
        vector.Normalize();
 
        float angle = Mathf.Atan2(vector.y, vector.x);

        float x = Mathf.Clamp(Mathf.Cos(angle) * screenWidth + screenWidth / 2, 0.0f, screenWidth);
        float y = Mathf.Clamp(Mathf.Sin(angle) * screenHeight + screenHeight / 2, 0.0f, screenHeight);

        return new Vector2(x, y);
}

    public IEnumerator KillTime(GameObject dyingPlayer)
    {
        Debug.Log("died");
        yield return new WaitForSeconds(2);
        dyingPlayer.SetActive(false);
        Debug.Break();
    }

    public void Reincarnate(Player player)
    {
        GameObject reincarnatedPlayer = Instantiate(reincarnationPrefab, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 6.5f, 0), Quaternion.identity);
        Player rpp = reincarnatedPlayer.GetComponent<Player>();
        rpp.playerName = player.playerName;
        rpp.Actions = player.Actions;
        reincarnatedPlayer.GetComponentInChildren<AmmoDisplay>().spriteColor = player.transform.GetComponentInChildren<AmmoDisplay>().spriteColor;
    }

    public IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static int SortByXPosition(Transform t1, Transform t2)
    {
        return t1.position.x.CompareTo(t2.position.x);
    }

    public void SwapActors(Transform actor1, Transform actor2)
    {
        Vector3 actor1Position = actor1.position;
        Vector3 actor2Position = actor2.position;

        actor1.position = actor2Position;
        actor2.position = actor1Position;
    }

    public void StartTeleportation(GameObject actor1, GameObject actor2)
    {
        GameObject effect = Instantiate(teleportEffect, actor1.transform.position, Quaternion.identity);
        Teleportation teleportation = effect.GetComponent<Teleportation>();
        teleportation.actor1 = actor1.transform;
        teleportation.actor2 = actor2.transform;
        teleportation.Initialize();
        StartCoroutine(teleportation.Teleport());
    }

    IEnumerator WaitToSpawnShip(float _seconds)
    {
        yield return new WaitForSeconds(_seconds);
        if (numberOfActivePlayers > 1)
        {
            levelGenerator.SpawnChunk(spaceShipModule, levelGenerator.GetPoint(levelGenerator.mostRecentModule, "ExitPoint").position);
            levelGenerator.generateLevels = false;
        }
    }
}