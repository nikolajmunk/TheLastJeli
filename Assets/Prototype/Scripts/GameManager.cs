using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject teleportEffect;
    public GameObject frontPlayer;
    GameObject[] players;
    public int numberOfPlayers;
    public List<Transform> playerPositions;

    static int SortByXPosition(Transform t1, Transform t2)
    {
        return t1.position.x.CompareTo(t2.position.x);
    }

    public IEnumerator Teleport(GameObject actor1, GameObject actor2)
    {
        Vector3 actor1Position = actor1.transform.position;
        Vector3 actor2Position = actor2.transform.position;

        // Insert teleport visual behavior here!
        GameObject effect = Instantiate(teleportEffect, (actor1Position + actor2Position) / 2, Quaternion.identity);
        RenderLine rl = effect.GetComponent<RenderLine>();
        rl.point1 = actor1.transform;
        rl.point2 = actor2.transform;

        //yield return new WaitForSeconds(2);
        actor1.transform.position = actor2Position;
        actor2.transform.position = actor1Position;
        Destroy(effect);

        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerPositions.Add(player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPositions.Sort(SortByXPosition);
        playerPositions.Reverse();
    }
}
