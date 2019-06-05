using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartSequence : MonoBehaviour
{
    public bool debug = false;

    private DestructionZone destructionZone;
    public AudioHandler audioHandler;

    public Sprite[] numberSprites;

    private int currentText = 0;
    public TMP_Text countdownText;
    public string[] countdown;

    public GameObject[] fallingBlocks;

    public List<GameObject> exclamations;

    // Start is called before the first frame update
    void Start()
    {
        if (debug == true)
        {
            GetComponent<Animator>().enabled = false;
            GameStarts();
        }
        else
        {
            fallingBlocks[3] = GameObject.FindGameObjectWithTag("MainCamera");
            destructionZone = GameObject.FindGameObjectWithTag("DestructionZone").GetComponent<DestructionZone>();
            DisableMovement();

            audioHandler = GetComponent<AudioHandler>();

            foreach (Player item in GameManager.instance.activePlayers)
            {
                //exclamations.Add(item.transform.Find("Exclamation").gameObject);
                item.transform.Find("Exclamation").gameObject.SetActive(false);

                foreach (Transform child in item.transform)
                {
                    if (child.gameObject.name == "Exclamation")
                    {
                        exclamations.Add(child.transform.gameObject);
                    }
                }
            }
        }
    }

    void DisableMovement()
    {
        // Player movement
        foreach (Player player in GameManager.instance.activePlayers)
        {
            player.Actions.Enabled = false;
        }

        // Destruction Zone movement
        destructionZone.move = false;
    }

    void EnableMovement()
    {
        // Player movement
        foreach (Player player in GameManager.instance.activePlayers)
        {
            player.Actions.Enabled = true;
        }

        // Destruction Zone movement
        destructionZone.move = true;
    }

    void PlaySound(string sound)
    {
        audioHandler.PlayOneShotByName(sound);
    }

    void ChangeCountdownText()
    {
        countdownText.text = countdown[currentText];
        currentText ++;
    }

    void DoTheShake()
    {
        foreach (GameObject item in fallingBlocks)
        {
            item.GetComponent<ObjectShake>().Shake();
        }
    }

    void PlayerReactsTrue()
    {
        foreach (GameObject item in exclamations)
        {
            item.SetActive(true);
        }

        foreach (Transform item in GameManager.instance.playerPositions)
        {
            item.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 500);
        }
    }

    void PlayerReactsFalse()
    {
        foreach (GameObject item in exclamations)
        {
            item.SetActive(false);
        }
    }

    public void GameStarts()
    {
        GameManager.instance.hasGameStarted = true;
    }
}