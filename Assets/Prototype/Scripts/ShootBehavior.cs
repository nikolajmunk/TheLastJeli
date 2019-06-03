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
    public int ammo;
    public int maxAmmo;
    public int ammoUnit;
    public float reloadTime;
    public bool continuousReload;

    bool reloading;

    void Start()
    {
        player = transform.root.GetComponent<Player>();
        actions = player.Actions;
        audioHandler = GetComponent<AudioHandler>();
        ammo = maxAmmo;
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
        if (ammo > 0)
        {
            FireShot();
        }
    }

    void FireShot()
    {
        muzzlePrefab.SetActive(false);
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        muzzlePrefab.SetActive(true);

        BulletBehavior bb = bullet.GetComponent<BulletBehavior>();
        bb.direction = shootOrigin.right;
        bb.moving = true;
        bb.origin = transform.root.gameObject;
        bb.GetComponent<Rigidbody>().AddForce(bb.transform.forward * shootSpeed);
        //Debug.DrawRay(shootOrigin.position, shootOrigin.right * 1000f, Color.red);
        audioHandler.PlayOneShotWithRandomPitch("Shoot", .5f, 1.5f);

        ammo -= ammoUnit; //Debug.Log("Ammo is " + ammo);
        int reloadAmount = 0;
        bool startReload = false;
        if (ammo == 0 && !continuousReload && !reloading)
        {
            reloadAmount = maxAmmo;
            startReload = true;
        }
        else if (ammo < maxAmmo && continuousReload && !reloading)
        {
            reloadAmount = ammoUnit;
            startReload = true;
        }
        if (startReload)
        {
            reloading = true;
            StartCoroutine(Reload(reloadTime, reloadAmount));
        }
    }

    IEnumerator Reload(float seconds, int newAmmo)
    {
        while (reloading)
        {
            if (ammo < maxAmmo)
            {
                //Debug.Log("reloading...");
                yield return new WaitForSeconds(seconds);
                ammo += newAmmo;
                //Debug.Log("done! ammo is " + ammo);
            }
            else
            {
                reloading = false;
                yield return null;
            }
        }
    }

}
