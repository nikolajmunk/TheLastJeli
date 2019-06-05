using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SceneBehavior : MonoBehaviour
{
    List<Transform> spawnPoints;
    private List<int> SpawnInts = new List<int> { 0, 1, 2, 3 };

    public float multiplePlayersSpaceshipSpawnTimer;
    public float twoPlayersSpaceshipSpawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.GetComponent<AudioSource>().clip = GameManager.instance.GetComponent<AudioHandler>().GetAudioClipByName("GameMusic");
        GameManager.instance.GetComponent<AudioSource>().Play();

        GameManager.instance.activePlayers = new List<Player>(PlayerManager.instance.players);
        GameManager.instance.numberOfActivePlayers = GameManager.instance.activePlayers.Count;
        if (GameManager.instance.playerPositions.Count == 0)
        {
            foreach (Player player in GameManager.instance.activePlayers)
            {
                GameManager.instance.playerPositions.Add(player.gameObject.transform);
            }
        }

        spawnPoints = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<SpawnPositions>().spawnPoints;

        foreach (Player player in PlayerManager.instance.players)
        {
            int temp = SpawnInts[Random.Range(0, SpawnInts.Count)];
            player.transform.position = spawnPoints[temp].position;
            ShootBehavior shootBehavior = player.GetComponentInChildren<ShootBehavior>();
            shootBehavior.ammo = shootBehavior.maxAmmo;
            SpawnInts.Remove(temp);
            if (!player.gameObject.activeSelf)
            {
                player.gameObject.SetActive(true);
            }
        }

        if (GameManager.instance.activePlayers.Count != 0)
        {
            PlayerManager.instance.acceptNewPlayers = false;
        }

        GameManager.instance.isEndGame = false;
        GameManager.instance.isEveryoneDead = false;
        GameManager.instance.isGameOver = false;
        GameManager.instance.winningPlayer = null;
        GameManager.instance.playerInSpaceship = false;
        GameManager.instance.destructionZone = GameObject.FindGameObjectWithTag("DestructionZone");
        if (GameManager.instance.numberOfActivePlayers > 2)
        {
            GameManager.instance.twoPlayerSpaceshipSpawnTime = multiplePlayersSpaceshipSpawnTimer;
        }
        else
        {
            GameManager.instance.twoPlayerSpaceshipSpawnTime = twoPlayersSpaceshipSpawnTimer;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
