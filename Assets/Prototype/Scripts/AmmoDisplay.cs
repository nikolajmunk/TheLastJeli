using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public GameObject ammoSprite;
    public GameObject arrowSprite;
    public GameObject ammoContainer;

    public Color spriteColor;

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
            shot.GetComponent<Image>().color = spriteColor;
        }
        GameObject arrow = Instantiate(arrowSprite, ammoContainer.transform);
        arrow.GetComponent<Image>().color = spriteColor;

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
