using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionZone : MonoBehaviour
{
    public float waitSeconds;
    public float speed;
    public bool move;

    private void Update()
    {
        if (move)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 newPosition = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null && other.gameObject.tag != "Kill")
        {
            Rigidbody rb = other.gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
            other.gameObject.GetComponent<MeshCollider>().convex = true;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            StartCoroutine(DestroyModule(other.gameObject, waitSeconds));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Terrain")
        {
            Transform activeEntryPoint = other.transform.root.Find("EntryPoint");
            Vector3 pointPosition = activeEntryPoint.position;
            if (activeEntryPoint != null)
            {
                GameManager.instance.killBoxPosition = pointPosition;
            }
        }
    }

    private IEnumerator DestroyModule(GameObject _module, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject objectToDestroy = null;

        if (_module.transform.root.childCount <= 3)
        {
            objectToDestroy = _module.transform.root.gameObject;
        }
        else
        {
            objectToDestroy = _module.gameObject;
        }
        Destroy(objectToDestroy);
    }
}
