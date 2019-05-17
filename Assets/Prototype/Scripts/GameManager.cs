﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject teleportEffect;
    public GameObject frontPlayer;
    public List<GameObject> players;
    public int numberOfPlayers;
    public List<Transform> playerPositions;
    public GameObject winUI;
    public TextMeshProUGUI winText;
    public bool debug;

    public void GameOver(float delay)
    {
        if (!debug)
        {
            winUI.SetActive(true);
            winText.text = players[0].name + " wins!";
            StartCoroutine(Restart(delay));
        }
    }
    public void KillPlayer(GameObject player)
    {
        numberOfPlayers -= 1;
        players.Remove(player);
        playerPositions.Remove(player.transform);
        Destroy(player);
    }
    public IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    static int SortByXPosition(Transform t1, Transform t2)
    {
        return t1.position.x.CompareTo(t2.position.x);
    }

    public void SwapActors(Transform actor1, Transform actor2)
    {
        Vector3 actor1Position = actor1.position;
        Vector3 actor2Position = actor2.position;

        actor1.position = actor2Position;
        actor2.position = actor1Position;
    }

    public void StartTeleportation(GameObject actor1, GameObject actor2)
    {
        GameObject effect = Instantiate(teleportEffect, actor1.transform.position, Quaternion.identity);
        Teleportation teleportation = effect.GetComponent<Teleportation>();
        teleportation.actor1 = actor1.transform;
        teleportation.actor2 = actor2.transform;
        StartCoroutine(teleportation.Teleport());
        Debug.Log("Did it");
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

        if (numberOfPlayers == 1)
        {
            GameOver(5);
        }
    }
}
