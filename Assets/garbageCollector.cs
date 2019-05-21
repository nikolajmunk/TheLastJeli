using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garbageCollector : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        //Destroy(other.gameObject);
        Rigidbody rb = other.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        other.gameObject.GetComponent<MeshCollider>().convex = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        //other.gameObject.GetComponent<MeshCollider>().enabled = false;
        //other.gameObject.AddComponent(typeof(BoxCollider));
    }
}
