using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    public GameObject ammoSprite;
    public GameObject arrowSprite;
    public GameObject ammoContainer;
    int maxAmmo;
    int ammo;
    ShootBehavior gun;
    List<GameObject> shots = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<ShootBehavior>();
        maxAmmo = gun.maxAmmo;

        for (int i = 0; i < maxAmmo; i++)
        {
            GameObject shot = Instantiate(ammoSprite,ammoContainer.transform);
            shots.Add(shot);
        }
        GameObject arrow = Instantiate(arrowSprite, ammoContainer.transform);

        shots.Reverse();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ammo = gun.ammo;
        foreach (GameObject shot in shots)
        {
            if (shots.IndexOf(shot) >= ammo)
            {
                shot.SetActive(false);
            }
            else
            {
                shot.SetActive(true);
            }
        }
    }
}
