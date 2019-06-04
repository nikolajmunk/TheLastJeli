using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerTurret : MonoBehaviour
{
    public int speed = 10;
    public float maxDistanceFromCenter;
    Player player;
    public GameObject spawnObject;

    private void OnEnable()
    {
        GameManager.instance.OnWin += OnGameEnds;
        GameManager.instance.OnAllPlayersDead += OnGameEnds;
    }

    void OnDisable()
    {
        GameManager.instance.OnWin -= OnGameEnds;
        GameManager.instance.OnAllPlayersDead -= OnGameEnds;
    }

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (player.Actions.Move)
        {
            var pos = transform.localPosition;
            pos.x += player.Actions.Move.Value.x * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -maxDistanceFromCenter, maxDistanceFromCenter);

            transform.localPosition = pos;
        }

        //if (player.Actions.Shoot && player.Actions.Shoot.LastValue == 0)
        //{
        //    GameObject spawnedObject = Instantiate(spawnObject, transform.position, Quaternion.identity);
        //    spawnedObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 3000, ForceMode.Impulse);
        //}
    }

    void OnGameEnds()
    {
        Destroy(gameObject);
    }

}
