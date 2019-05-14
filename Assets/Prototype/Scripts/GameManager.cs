using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject teleportEffect;
    public GameObject frontPlayer;
    public List<GameObject> players;
    public int numberOfPlayers;
    public List<Transform> playerPositions;

    public void KillPlayer(GameObject player)
    {
        numberOfPlayers -= 1;
        players.Remove(player);
        playerPositions.Remove(player.transform);
        Destroy(player);
    }

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

    void Awake()
    {
        instance = this;
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        numberOfPlayers = players.Count;
        foreach (GameObject player in players)
        {
            playerPositions.Add(player.transform);
        }
        frontPlayer = playerPositions[0].gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerPositions.Sort(SortByXPosition);
        playerPositions.Reverse();
        frontPlayer = playerPositions[0].gameObject;
    }
}
