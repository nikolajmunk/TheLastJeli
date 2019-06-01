using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject teleportEffect;
    [HideInInspector]
    public GameObject frontPlayer;
    public List<Player> activePlayers;
    public int numberOfActivePlayers;
    public List<Transform> playerPositions;
    public GameObject winUI; // This should not be in the game manager. Or at least we should specify that the lobby should not show win text.
    public TextMeshProUGUI winText;
    public bool debug;
    public PlayerManager playerManager;
    public LevelGenerator levelGenerator;
    [HideInInspector]
    public GameObject spaceShipModule;
    public GameObject destructionZone;
    public GameObject reincarnationPrefab;

    public Vector3 killBoxPosition;

    public bool isEndGame = false;
    private bool isEveryoneDead = false;
    private bool isGameOver = false;

    public bool playerInSpaceship = false;

    public delegate void GameStateEvent();
    public GameStateEvent OnWin;
    public GameStateEvent OnAllPlayersDead;
    public GameStateEvent OnEndGame;
    public GameStateEvent OnReadyToPlay;

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

        // Only one player left
        if (numberOfActivePlayers == 1 && isEndGame == false && SceneManager.GetActiveScene().name == "Scene_master")
        {
            EndGame();
        }

        // If one player reaches the spaceship, she wins
        if (playerInSpaceship == true && isGameOver == false)
        {
            GameOver(5);
        }

        // If all players die
        if (numberOfActivePlayers == 0 && isEndGame == true && isEveryoneDead == false)
        {
            // Do stuff here for when everyone is out of the game

            OnAllPlayersDead();
            isEveryoneDead = true;
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
        levelGenerator.SpawnChunk(spaceShipModule, levelGenerator.GetPoint(levelGenerator.mostRecentModule, "ExitPoint").position);
        levelGenerator.generateLevels = false;
        OnEndGame();
        isEndGame = true;
    }

    public void GameOver(float delay)
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
        OnPlayerRemoved(player);

        player.GetComponent<AudioHandler>().PlayOneShotByName("Death");
        StartCoroutine(KillTime(player.gameObject));
        Reincarnate(player);
    }
    public IEnumerator KillTime(GameObject dyingPlayer)
    {
        yield return new WaitForSeconds(2);
        dyingPlayer.SetActive(false);
    }

    public void Reincarnate(Player player)
    {
        GameObject reincarnatedPlayer = Instantiate(reincarnationPrefab, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y +6.5f, 0), Quaternion.identity, Camera.main.transform);
        Player rpp = reincarnatedPlayer.GetComponent<Player>();
        rpp.playerName = player.playerName;
        rpp.Actions = player.Actions;
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
}