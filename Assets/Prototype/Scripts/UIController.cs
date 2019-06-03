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
        winCanvas.SetActive(true);
        winText.text = "Win Placeholder";
    }

    void OnEndGame()
    {
        endgameCanvas.SetActive(true);
        endgameText.text = "Endgame Placeholder";
    }

    void OnAllPlayersDead()
    {
        gameOverCanvas.SetActive(true);
        gameOverText.text = "Game Over Placeholder";
    }
}
