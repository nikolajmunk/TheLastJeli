using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Win UI")]
    public GameObject winCanvas;
    public TextMeshProUGUI winText;

    [Header("Endgame UI")]
    public GameObject endgameCanvas;
    public TextMeshProUGUI endgameText;

    [Header("Game Over UI")]
    public GameObject gameOverCanvas;
    public TextMeshProUGUI gameOverText;

    private UIbuttons uiButtonsScript;
    

    private void Start() {
        uiButtonsScript = GetComponent<UIbuttons>();
    }

    private void OnEnable()
    {
        GameManager.instance.OnWin += OnWin;
        GameManager.instance.OnEndGame += OnEndGame;
        GameManager.instance.OnAllPlayersDead += OnAllPlayersDead;
    }

    private void OnDisable()
    {
        GameManager.instance.OnWin -= OnWin;
        GameManager.instance.OnEndGame -= OnEndGame;
        GameManager.instance.OnAllPlayersDead -= OnAllPlayersDead;
    }

    void OnWin()
    {
        endgameCanvas.SetActive(false);
        winCanvas.SetActive(true);
        winText.text = "You succesfully escaped the planet!";
        uiButtonsScript.ShowRestartUI();
    }

    void OnEndGame()
    {
        //endgameCanvas.SetActive(true);
        //endgameText.text = "Endgame Placeholder";
    }

    void OnAllPlayersDead()
    {
        endgameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverText.text = "All Jeli has failed \n Great Job..";
        uiButtonsScript.ShowRestartUI();
    }
}
