using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject startB;
    public GameObject restartB;
    public GameObject endB;

    // Start is called before the first frame update
    void Start()
    {
        startB = GameObject.Find("Start button");
        restartB = GameObject.Find("Restart button");
        endB = GameObject.Find("Quit button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

    }
    public void RestartGame()
    {

    }
    public void EndGame()
    {

    }
}
