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

    public float shootInput;
    bool isShootInUse;


    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootOrigin.position, shootOrigin.rotation);
        GameObject muzzle = Instantiate(muzzlePrefab, shootOrigin.position, shootOrigin.rotation);
        BulletBehavior bb = bullet.GetComponent<BulletBehavior>();
        bb.direction = shootOrigin.right;
        bb.moving = true;
        bb.origin = transform.root.gameObject;
        bb.GetComponent<Rigidbody>().AddForce(bb.transform.forward * shootSpeed);
        //Debug.DrawRay(shootOrigin.position, shootOrigin.right * 1000f, Color.red);
        audioHandler.PlayOneShotByName("Shoot");
    }

    // Start is called before the first frame update
    void Start()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootInput != 0)
        {
            if (isShootInUse == false)
            {
                Shoot();
                isShootInUse = true;
            }
        }
        if (shootInput == 0)
        {
            isShootInUse = false;
        }
    }
    
}
