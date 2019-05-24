using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehavior : MonoBehaviour
{
    List<Transform> spawnPoints;
    private List<int> SpawnInts = new List<int> { 0, 1, 2, 3 };


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.activePlayers = new List<Player>(PlayerManager.instance.players);
        spawnPoints = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<SpawnPositions>().spawnPoints;

        foreach (Player player in PlayerManager.instance.players)
        {
            int temp = SpawnInts[Random.Range(0, SpawnInts.Count)];
            player.transform.position = spawnPoints[temp].position;
            ShootBehavior shootBehavior = player.GetComponentInChildren<ShootBehavior>();
            shootBehavior.ammo = shootBehavior.maxAmmo;
            SpawnInts.Remove(temp);
        }

        if (GameManager.instance.activePlayers.Count != 0)
        {
            PlayerManager.instance.acceptNewPlayers = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
