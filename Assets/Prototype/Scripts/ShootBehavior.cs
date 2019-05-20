using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    public Transform shootOrigin;
    public float shootSpeed;
    public GameObject bulletPrefab;
    public GameObject muzzlePrefab;
    AudioHandler audioHandler;
    Player player;
    PlayerActions actions;

    void Start()
    {
        player = transform.root.GetComponent<Player>();
        actions = player.Actions;
        audioHandler = GetComponent<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actions.Shoot && actions.Shoot.LastValue == 0) // If button is pressed and it wasn't in the last frame...
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        GameObject muzzleFlash = Instantiate(muzzlePrefab, shootOrigin.position, shootOrigin.rotation);
        ParticleSystem parts = muzzleFlash.GetComponent<ParticleSystem>();
        float totalDuration = parts.main.duration + parts.main.startLifetime.constantMax;
        Destroy(muzzleFlash, totalDuration);


        BulletBehavior bb = bullet.GetComponent<BulletBehavior>();
        bb.direction = shootOrigin.right;
        bb.moving = true;
        bb.origin = transform.root.gameObject;
        bb.GetComponent<Rigidbody>().AddForce(bb.transform.forward * shootSpeed);
        //Debug.DrawRay(shootOrigin.position, shootOrigin.right * 1000f, Color.red);
        audioHandler.PlayOneShotByName("Shoot");
    }

}
