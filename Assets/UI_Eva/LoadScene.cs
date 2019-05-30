using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        playerManager.RemoveAllPlayers();
        playerManager.acceptNewPlayers = false;
    }

    // Start is called before the first frame update
    public void ToLobby()
    {
        SceneManager.LoadScene("Lobby_master");
        
    }
    public void Quit()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    } 
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
