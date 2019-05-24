using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToLobby()
    {
        SceneManager.LoadScene("Lobby");
        
    }
    public void Quit()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    } 
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
