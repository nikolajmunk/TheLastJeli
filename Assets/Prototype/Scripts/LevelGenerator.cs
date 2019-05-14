using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> modules;
    public GameObject mostRecentModule;
    public float minDistanceBetweenCameraAndNewestEntryPoint;

    Transform GetPoint(GameObject module, string name)
    {
        return module.transform.Find(name);
    }

    void SpawnChunk(Vector3 spawnPosition) //Add argument here: module gameobject. This allows us to abstract stuff out so we can spawn essentially whichever module we want without being dependent on randomization.
    {
        int randomNumber = Random.Range(0, modules.Count);
        GameObject module = modules[randomNumber];

        GameObject chunk = Instantiate(module, spawnPosition, module.transform.rotation);
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
        float distanceBetweenCameraAndNewestEntryPoint = GetPoint(mostRecentModule, "EntryPoint").position.x - Camera.main.transform.position.x;
        if (distanceBetweenCameraAndNewestEntryPoint <= minDistanceBetweenCameraAndNewestEntryPoint)
        {
            Vector3 spawnPoint = GetPoint(mostRecentModule, "ExitPoint").position;
            SpawnChunk(spawnPoint);
        }
    }
}
