using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositions : MonoBehaviour
{
    public bool randomizePositions;
    public List<Transform> spawnPoints;

    // Start is called before the first frame update
    void Awake()
    {
        if (randomizePositions)
        {
            spawnPoints = Shuffle<Transform>(spawnPoints);
        }

        spawnPoints = new List<Transform>(spawnPoints);
    }

    public List<T> Shuffle<T>(List<T> list)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int k = rnd.Next(0, i);
            T value = list[k];
            list[k] = list[i];
            list[i] = value;
        }
        return list;
    }
}
