using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayerTurret : MonoBehaviour
{
    public int speed = 10;
    public float maxDistanceFromCenter;
    Player player;
    public GameObject spawnObject;
    public Transform target;

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
        target = Camera.main.transform;
    }

    private void FixedUpdate()
    {
        //Vector3 newPosition = target.position;
        //newPosition.y += 6.5f;
        //newPosition.z = 0;
        //transform.position = newPosition;

        if (player.Actions.Move)
        {
            var pos = transform.localPosition;
            pos.x += player.Actions.Move.Value.x * speed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, -maxDistanceFromCenter, maxDistanceFromCenter);

            transform.localPosition = pos;
            var newPosition = transform.position;
            newPosition.z = 0;
            transform.position = newPosition;
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

    private void Update()
    {

    }

}
