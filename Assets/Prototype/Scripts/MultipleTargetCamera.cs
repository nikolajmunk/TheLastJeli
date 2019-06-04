using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    Vector3 offset;
    Vector3 velocity;
    public float smoothTime = .5f;
    public Transform offsetPoint;
    List<Transform> targets;

    private void Start()
    {
        targets = GameManager.instance.playerPositions;
        offset = transform.position - offsetPoint.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetTargetPosition(), ref velocity, smoothTime);
    }

    Vector3 GetTargetPosition()
    {
        if (targets.Count == 0)
        {
            return transform.position;
        }
        var frontmostPosition = targets[0].position;
        var pos = new Vector3(frontmostPosition.x, GetCentroid().y, frontmostPosition.z) + offset;
        return pos;
    }

    Bounds GetEncapsulatingBounds()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        

        return bounds;
    }

    Vector3 GetCentroid()
    {
        return GetEncapsulatingBounds().center;
    }

}
