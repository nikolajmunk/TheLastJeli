using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    public float thrust;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * thrust);
    }

    void FixedUpdate()
    {
        //rb.AddForce(transform.forward * thrust);
    }
}
