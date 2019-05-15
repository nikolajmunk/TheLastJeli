using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //public Transform target;
    public Vector3 offset;
    public List<GameObject> players;
    int numberOfPlayers;
    public float maxMovementDelta;

    private void Start()
    {
        offset = transform.position - GameManager.instance.frontPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yCentroid = GetCentroid().y;
        //transform.position = GetCentroid() + offset;
        //transform.position = new Vector3(GameManager.instance.playerPositions[0].position.x + offset.x, Vector3.MoveTowards(transform.position, GameManager.instance.playerPositions[0].position + offset, maxMovementDelta).y, GameManager.instance.playerPositions[0].position.z + offset.z);
        transform.position = new Vector3(GameManager.instance.playerPositions[0].position.x + offset.x, yCentroid + offset.y, GameManager.instance.playerPositions[0].position.z + offset.z);

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
