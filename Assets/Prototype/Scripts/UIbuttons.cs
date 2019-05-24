using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttons : MonoBehaviour
{
    public GameObject restartB;
    public GameObject endB;
    public GameObject startB;

    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        restartB = GameObject.Find("Restart button");
        endB = GameObject.Find("Quit button");
        startB = GameObject.Find("Start button");

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "ActualScene")
        {
            restartB.SetActive(false);
            endB.SetActive(false);
            startB.SetActive(false);
        }

        
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
        SceneManager.LoadScene("MasterScene");
    }
}
