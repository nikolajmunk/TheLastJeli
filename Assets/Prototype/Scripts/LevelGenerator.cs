using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> modules;
    public GameObject mostRecentModule;

    Transform GetExitPoint(GameObject module)
    {
        return module.transform.Find("ExitPoint");
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int randomNumber = Random.Range(0, modules.Count);
        GameObject module = modules[randomNumber];

        GameObject chunk = Instantiate(module, spawnPosition, Quaternion.identity);
        Transform entryPoint = chunk.transform.Find("EntryPoint");
        chunk.transform.position -= (entryPoint.position-spawnPosition);

        mostRecentModule = chunk;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 spawnPoint = GetExitPoint(mostRecentModule).position;
            SpawnChunk(spawnPoint);
        }
    }
}
