using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //public Transform target;
    public Vector3 offset;
    public GameObject[] players;
    int numberOfPlayers;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayers = players.Length;
        offset = transform.position - GetCentroid();
    }

    // Update is called once per frame
    void Update()
    {
        GetCentroid();
        //transform.position = GetCentroid() + offset;
        transform.position = GameManager.instance.playerPositions[0].position + offset;
    }

    private Vector3 GetCentroid()
    {
        Vector3 centroid = Vector3.zero;
        foreach (GameObject player in players)
        {
            centroid += player.transform.position;
        }
        centroid /= numberOfPlayers;
        return centroid;
    }
}
