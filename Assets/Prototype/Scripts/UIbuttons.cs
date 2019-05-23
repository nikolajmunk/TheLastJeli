using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttons : MonoBehaviour
{
    GameObject restartB;
    GameObject endB;
    GameObject startB;

    // Start is called before the first frame update
    void Start()
    {
        /*
        restartB = GameObject.Find("Restart button");
        endB = GameObject.Find("Quit button");
        startB = GameObject.Find("Start button");

        restartB.SetActive(false);
        endB.SetActive(false);
        startB.SetActive(false);
        */
    }


    public void Restart()
    {
        SceneManager.LoadScene("ActualScene");
    }

    public void EndGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ActualScene");
    }
}
