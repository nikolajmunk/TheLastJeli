using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject teleportEffect;
    public GameObject frontPlayer;
    public List<Player> activePlayers; // Maybe this shouldn't copy the players list from PlayerManager. After all, we want a list of active players, and if we're always just cloning it, what's the point?
    public int numberOfActivePlayers; // This should be number of active players, not number of registered players. Leave that part to PlayerManager.
    public List<Transform> playerPositions;
    public GameObject winUI; // This should not be in the game manager. Or at least we should specify that the lobby should not show win text.
    public TextMeshProUGUI winText;
    public bool debug;
    public PlayerManager playerManager;
    public UI buttonScript;

    private void OnEnable()
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
        playerManager.OnPlayerAdded += OnPlayerAdded;
        playerManager.OnPlayerRemoved += OnPlayerRemoved;
    }

    void Awake()
    {
        if (playerManager == null)
        {
            playerManager = GetComponent<PlayerManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPositions.Sort(SortByXPosition);
        playerPositions.Reverse();
        if (playerPositions.Count != 0)
        {
            frontPlayer = playerPositions[0].gameObject;
        }

        if (numberOfActivePlayers == 1)
        {
            GameOver(5);
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

    public void GameOver(float delay)
    {
        if (!debug)
        {
            buttonScript.restartB.SetActive(true);
            buttonScript.endB.SetActive(true);

            winUI.SetActive(true);
            winText.text = activePlayers[0].name + " wins!";
            StartCoroutine(Restart(delay));
        }
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
        //playerManager.RemovePlayer(player); // Don't do this; we only want to remove the player from the list of active players, not the master list. The master list is for remembering players between scenes. Go fuck yourself.

        //Player sound
        player.GetComponent<AudioHandler>().PlayOneShotByName("Death");
        KillTime(player.gameObject);
    }
    public IEnumerator KillTime(GameObject dyingPlayer)
    {
        yield return new WaitForSeconds(2);
        dyingPlayer.SetActive(false);
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
        StartCoroutine(teleportation.Teleport());
        Debug.Log("Did it");
    }
}
