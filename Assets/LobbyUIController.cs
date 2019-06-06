using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    public GameObject beginUI;

    void OnEnable()
    {
        GameManager.instance.OnReadyToPlay += OnReadyToPlay;
    }

    void OnDisable()
    {
        GameManager.instance.OnReadyToPlay -= OnReadyToPlay;

    }

    void OnReadyToPlay()
    {
        beginUI.SetActive(true);
    }
}
