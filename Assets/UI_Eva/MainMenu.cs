using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    PlayerManager playerManager;
    public ParticleSystem cowsteroids;
    private ParticleSystem.MainModule cowMain;
    private ParticleSystem.EmissionModule cowEmission;
    private int newMax = 2;
    private AudioSource moo;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        playerManager.RemoveAllPlayers();
        playerManager.acceptNewPlayers = false;
        GameManager.instance.GetComponent<AudioSource>().clip = GameManager.instance.GetComponent<AudioHandler>().GetAudioClipByName("MenuMusic");
        GameManager.instance.GetComponent<AudioSource>().Play();

        cowEmission = cowsteroids.emission;
        cowMain = cowsteroids.main;

        moo = GetComponent<AudioSource>();

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

    public void MoreCows()
    {
        newMax += 1;

        if(!moo.isPlaying)
            moo.Play();

        if(cowMain.maxParticles <= 1500)
            cowMain.maxParticles += 50;

        if(newMax <= 10)
            cowEmission.rateOverTime = newMax;

    }
}
