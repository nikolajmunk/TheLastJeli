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

        restartB = transform.GetChild(0).gameObject;
        endB = transform.GetChild(1).gameObject;
        startB = transform.GetChild(2).gameObject;

        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "ActualScene")
        {
            restartB.SetActive(false);
            endB.SetActive(false);
            startB.SetActive(false);
        }

        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MasterScene");
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentScene);
    }

    public void EndGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }


}
