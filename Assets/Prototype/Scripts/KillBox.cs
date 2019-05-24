using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public float offsetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xPosition = 0;
        if (GameManager.instance.playerPositions.Count != 0)
        {
            xPosition = GameManager.instance.playerPositions[0].position.x;
        }
        transform.position = new Vector3(xPosition, GameManager.instance.killBoxPosition.y - offsetY, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.KillPlayer(other.GetComponent<Player>());
        }
    }
}
