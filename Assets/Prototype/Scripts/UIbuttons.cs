using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIbuttons : MonoBehaviour
{
    public GameObject restartB;
    public GameObject endB;
    public GameObject startB;

    string currentScene;

    // Start is called before the first frame update
    void Start()
    {
        //restartB = GameObject.Find("Restart button");
        //endB = GameObject.Find("Quit button");
        //startB = GameObject.Find("Start button");

        startB = transform.GetChild(0).gameObject;
        restartB = transform.GetChild(1).gameObject;
        endB = transform.GetChild(2).gameObject;

        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "ActualScene")
        {
            restartB.SetActive(false);
            endB.SetActive(false);
            startB.SetActive(false);
        }


    }

    private void Update()
    {
        // Pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowPauseUI();
        }

        // Lobby controls

    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene_master");
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Main_Menu");

        //Application.Quit();

        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void ShowPauseUI()
    {
        restartB.SetActive(!restartB.activeInHierarchy);
        endB.SetActive(!endB.activeInHierarchy);
        startB.SetActive(!startB.activeInHierarchy);
    }

    public void ShowRestartUI()
    {
        restartB.SetActive(!restartB.activeInHierarchy);
        endB.SetActive(!endB.activeInHierarchy);
    }

}
