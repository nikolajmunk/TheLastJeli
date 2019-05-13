using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLine : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    LineRenderer lr;
    Vector3[] positions = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        positions[0] = point1.position;
        positions[1] = point2.position;
        lr.SetPositions(positions);
    }
}
