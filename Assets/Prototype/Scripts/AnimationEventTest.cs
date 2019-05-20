using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventTest : MonoBehaviour
{
    public void MakeCubeRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
