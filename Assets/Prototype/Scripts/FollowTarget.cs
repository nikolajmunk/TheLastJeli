using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //public Transform target;
    public Vector3 offset;
    public List<GameObject> players;
    int numberOfPlayers;

    private void Start()
    {
        offset = transform.position - GameManager.instance.frontPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //GetCentroid();
        //transform.position = GetCentroid() + offset;
        transform.position = GameManager.instance.playerPositions[0].position + offset;
    }

    private Vector3 GetCentroid()
    {
        Vector3 centroid = Vector3.zero;
        foreach (Transform player in GameManager.instance.playerPositions)
        {
            centroid += player.position;
        }
        centroid /= GameManager.instance.numberOfPlayers;
        return centroid;
    }
}
