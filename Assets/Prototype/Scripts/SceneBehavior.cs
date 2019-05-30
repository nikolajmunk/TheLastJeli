﻿using System.Collections;
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
        GameManager.instance.destructionZone = GameObject.FindGameObjectWithTag("DestructionZone");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
