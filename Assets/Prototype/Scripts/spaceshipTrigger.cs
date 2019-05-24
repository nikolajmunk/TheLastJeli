using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipTrigger : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {

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

            // Player child to UFO
            // Lock player to speciffic position with no collider, disable character controller
            
            //trigger anim.SetTrigger("engageCollider");
            
        }
    }
}
