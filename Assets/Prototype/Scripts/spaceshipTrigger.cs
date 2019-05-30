using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceshipTrigger : MonoBehaviour
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
            GameObject playerClone = Instantiate(other.gameObject, this.transform.position - new Vector3(0, .62f, 0), Quaternion.Euler(-5f, -153, 0), this.transform);
            other.gameObject.SetActive(false);
            //playerClone.transform.localPosition = new Vector3(0, -1.3f, 0);
            //playerClone.transform.localEulerAngles = new Vector3(-5f, -153, 0);

            GameManager.instance.playerInSpaceship = true;
            playerClone.GetComponent<PlatformerController>().canMove = false;
            playerClone.GetComponent<PlatformerController>().enabled = false;
            playerClone.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerClone.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY;
            playerClone.GetComponent<PlatformerController>().anim.SetFloat("speed", 0);
            playerClone.GetComponent<PlatformerController>().anim.SetFloat("velocity", 0);
            playerClone.GetComponent<CapsuleCollider>().enabled = false;
            playerClone.transform.GetChild(0).gameObject.SetActive(false);

            anim.SetTrigger("engageCollider");

            // make the environment destroy under the ship when you fly away
        }
    }
}
