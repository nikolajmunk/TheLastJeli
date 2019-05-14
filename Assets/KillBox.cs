﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBox : MonoBehaviour
{
    public Vector3 offset;

    public IEnumerator Restart(float delay)
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x - offset.x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        StartCoroutine(Restart(2));
    }
}
