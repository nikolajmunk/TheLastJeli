using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject teleportEffect;

    public IEnumerator Teleport(GameObject actor1, GameObject actor2)
    {
        Vector3 actor1Position = actor1.transform.position;
        Vector3 actor2Position = actor2.transform.position;

        // Insert teleport visual behavior here!
        GameObject effect = Instantiate(teleportEffect, (actor1Position + actor2Position) / 2, Quaternion.identity);
        RenderLine rl = effect.GetComponent<RenderLine>();
        rl.point1 = actor1.transform;
        rl.point2 = actor2.transform;

        //yield return new WaitForSeconds(2);
        actor1.transform.position = actor2Position;
        actor2.transform.position = actor1Position;
        Destroy(effect);

    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
