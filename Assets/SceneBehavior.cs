using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBehavior : MonoBehaviour
{
    public GameObject StartingModule;
    public List<Transform> spawnPoints;
    private List<int> SpawnInts = new List<int> { 0, 1, 2, 3 };


    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        GameManager.instance.activePlayers = new List<Player>(PlayerManager.instance.players);
        StartingModule = GetComponent<LevelGenerator>().startingModule;
=======
<<<<<<< HEAD
        GameManager.instance.activePlayers = PlayerManager.instance.players;
=======
        GameManager.instance.activePlayers = new List<Player>(PlayerManager.instance.players);
        StartingModule = GetComponent<LevelGenerator>().startingModule;
>>>>>>> 1301bd727bb7bb346367bb18e2bc4f319146e3b2
>>>>>>> parent of 1887384... Merge branch 'master' of https://github.com/nikolajmunk/TheLastJeli

        if (StartingModule != null)
        {
            foreach (Transform child in StartingModule.transform)
            {
                if (child.gameObject.tag == "SpawnPoint")
                {
                    spawnPoints.Add(child);
                }
            }
        }

        foreach (Player player in PlayerManager.instance.players)
        {
            int temp = SpawnInts[Random.Range(0, SpawnInts.Count)];
            player.transform.position = spawnPoints[temp].position;
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
