using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehavior : MonoBehaviour
{
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.activePlayers = PlayerManager.instance.players;

        if (spawnPoint == null)
        {
            spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        }

        foreach (Player player in PlayerManager.instance.players)
        {
            player.transform.position = spawnPoint.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
