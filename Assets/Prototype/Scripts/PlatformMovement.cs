using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool MoveSideways = false;
    private Vector3 pos1;
    private Vector3 pos2;
    public float speed = 1.0f;
    public float travelDistance = 4f;

    void Start()
    {
        if (MoveSideways)
        {
            pos1 = new Vector3(this.gameObject.transform.position.x - travelDistance, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            pos2 = new Vector3(this.gameObject.transform.position.x + travelDistance, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        else
        {
            pos1 = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - travelDistance, this.gameObject.transform.position.z);
            pos2 = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + travelDistance, this.gameObject.transform.position.z);
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
    }
}
