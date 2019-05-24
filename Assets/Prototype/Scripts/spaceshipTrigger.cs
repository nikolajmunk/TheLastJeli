using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipTrigger : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().playerInSpaceship = true;
            other.gameObject.GetComponent<PlatformerController>().canMove = false;
            other.gameObject.GetComponent<PlatformerController>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            other.gameObject.GetComponent<PlatformerController>().anim.SetFloat("speed", 0);
            other.gameObject.GetComponent<PlatformerController>().anim.SetFloat("velocity", 0);
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.SetParent(this.transform);
            other.gameObject.transform.localPosition = new Vector3(0, -1.3f, 0);
            other.gameObject.transform.localEulerAngles = new Vector3(-5f, -153, 0);
            anim.SetTrigger("engageCollider");
        }
    }
}
