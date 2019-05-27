using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    //public Vector3 offset;
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        //offset = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(Camera.main.transform.position.x - offset.x, Camera.main.transform.position.y - offset.y, 0);
        transform.position = GameManager.instance.killBoxPosition - new Vector3(0, offsetY, 0);
        foreach (Transform player in GameManager.instance.playerPositions)
        {
            if (player.position.y < transform.position.y) {
                GameManager.instance.KillPlayer(player.GetComponent<Player>());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.KillPlayer(other.GetComponent<Player>());
        }
    }
}
