using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> modules;
    public GameObject mostRecentModule;
    public float minDistanceBetweenCameraAndNewestEntryPoint;

    Transform GetPoint(GameObject module, string name) // Returns the child of the module GameObject that has the specified name.
    {
        return module.transform.Find(name);
    }

    void SpawnRandomModule(List<GameObject> list) // Pulls a random module from the provided list, then spawns it at the end of the previous module.
    {
        GameObject module = GetRandomModule(list);
        Vector3 spawnPoint = GetPoint(mostRecentModule, "ExitPoint").position;
        SpawnChunk(module, spawnPoint);
    }

    void SpawnChunk(GameObject module, Vector3 spawnPosition) // Spawns a specified module at the end of the previous module.
    {

        GameObject chunk = Instantiate(module, spawnPosition, module.transform.rotation);
        Transform entryPoint = chunk.transform.Find("EntryPoint");
        chunk.transform.position -= (entryPoint.position - spawnPosition);
        mostRecentModule = chunk;
    }

    private GameObject GetRandomModule(List<GameObject> list) // Returns a random module from the provided list. Currently does not support weighted randomization; everything has an equal chance of appearing.
    {
        int randomNumber = Random.Range(0, modules.Count);
        GameObject module = list[randomNumber];
        return module;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the camera gets too close to the end of the level, spawn a new module.
        float distanceBetweenCameraAndNewestEntryPoint = GetPoint(mostRecentModule, "EntryPoint").position.x - Camera.main.transform.position.x;
        if (distanceBetweenCameraAndNewestEntryPoint <= minDistanceBetweenCameraAndNewestEntryPoint)
        {
            SpawnRandomModule(modules);
        }
    }
}
