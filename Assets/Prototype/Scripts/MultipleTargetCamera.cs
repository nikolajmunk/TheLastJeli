using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    Vector3 offset;
    Vector3 velocity;
    public float smoothTime = .5f;
    public Transform offsetPoint;

    private void Start()
    {
        offset = transform.position - offsetPoint.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref velocity, smoothTime);
    }

    Vector3 GetTargetPosition()
    {
        if (GameManager.instance.playerPositions.Count == 0)
        {
            return transform.position;
        }
        var frontmostPosition = GameManager.instance.playerPositions[0].position;
        Debug.Log(GameManager.instance.playerPositions[0].gameObject.name);
        var pos = new Vector3(frontmostPosition.x, GetCentroid().y, frontmostPosition.z) + offset;
        return pos;
    }

    Bounds GetEncapsulatingBounds()
    {
        var bounds = new Bounds(GameManager.instance.playerPositions[0].position, Vector3.zero);
        for (int i = 1; i < GameManager.instance.playerPositions.Count; i++)
        {
            bounds.Encapsulate(GameManager.instance.playerPositions[i].position);
        }
        

        return bounds;
    }

    Vector3 GetCentroid()
    {
        return GetEncapsulatingBounds().center;
    }

}
