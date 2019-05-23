using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionZone : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null)
        {
            Rigidbody rb = other.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            other.gameObject.GetComponent<MeshCollider>().convex = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            StartCoroutine(DestroyModule(other.gameObject));
        }
    }

    private IEnumerator DestroyModule(GameObject _module)
    {
        yield return new WaitForSeconds(2f);
        Destroy(_module);
    }
}
