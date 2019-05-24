using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionMovement : MonoBehaviour
{
    // This script allows the destruction zone to move along the X-axis. It can be accessed by other scripts to increase or decrease pseed or stop it entirely.

    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
