using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    //public Transform target;
    public Vector3 offset;
    public List<GameObject> players;
    //int numberOfPlayers;
    public float maxMovementDelta;
    public float maxYOffsetToUpmost = 10;

    Vector3 newPositions;

    private void Start()
    {
        //offset = transform.position - GameManager.instance.frontPlayer.transform.position;
        newPositions = Vector3.zero;
        offset = transform.position - Vector3.zero;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.numberOfActivePlayers != 0)
        {
            float yCentroid = GetCentroid().y;
            //transform.position = GetCentroid() + offset;
            //transform.position = new Vector3(GameManager.instance.playerPositions[0].position.x + offset.x, Vector3.MoveTowards(transform.position, GameManager.instance.playerPositions[0].position + offset, maxMovementDelta).y, GameManager.instance.playerPositions[0].position.z + offset.z);
            newPositions = new Vector3(GameManager.instance.playerPositions[0].position.x + offset.x, yCentroid + offset.y, GameManager.instance.playerPositions[0].position.z + offset.z);
            if (newPositions.x > transform.position.x)
            {
                transform.position = newPositions;
            }
            else
            {
                transform.position = new Vector3(transform.position.x, yCentroid + offset.y, GameManager.instance.playerPositions[0].position.z + offset.z);
            }
        }
    }

    private Vector3 GetCentroid()
    {
        Vector3 centroid = Vector3.zero;
        float maxY = float.NegativeInfinity;
        foreach (Transform player in GameManager.instance.playerPositions)
        {
            if(player.position.y > maxY)
            {
                maxY = player.position.y;
            }
            centroid += player.position;
        }
        centroid /= GameManager.instance.numberOfActivePlayers;
        if(maxY - centroid.y > maxYOffsetToUpmost)
        {
            centroid.y = maxY - maxYOffsetToUpmost;
        }

        return centroid;
    }
}
