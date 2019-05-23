using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform actor1;
    public Transform actor2;
    public LineRenderer lr;
    public GameObject[] particles1;
    public GameObject[] particles2;

    public Animator anim;

    public void SetCanBeTeleported(int canBeTeleported)
    {
        actor2.GetComponent<Teleportable>().canBeTeleported = (canBeTeleported == 1 ? true : false);
    }

    public void SwitchPlaces()
    {
        GameManager.instance.SwapActors(actor1, actor2);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public IEnumerator Teleport()
    {
        anim.Play("Teleportation");
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        lr.GetComponent<RenderLine>().point1 = actor1;
        lr.GetComponent<RenderLine>().point2 = actor2;

        AssignFollowTarget(particles1, actor1);
        AssignFollowTarget(particles2, actor2);

    }

    private void AssignFollowTarget(GameObject[] objects, Transform target)
    {
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<FollowTransform>().target = target;
        }
    }

<<<<<<< HEAD
=======
    private void AssignParticleValues(GameObject[] objects, Teleportable actorTeleportable)
    {
        ParticleSystem ps;
        foreach (GameObject obj in objects)
        {
            Debug.Log("assigned.");
            ps = obj.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psMain = ps.main;
            psMain.startColor = actorTeleportable.particlesGradient;
        }
    }

    public void Initialize()
    {
        lr.GetComponent<RenderLine>().point1 = actor1;
        lr.GetComponent<RenderLine>().point2 = actor2;

        actor1Teleportable = actor1.GetComponent<Teleportable>();
        actor2Teleportable = actor2.GetComponent<Teleportable>();

        InstancedMaterial = lr.material;
        InstancedMaterial.SetColor("_Color0", actor1Teleportable.beamColor0);
        lr.material = InstancedMaterial;
        AssignFollowTarget(particles1, actor1);
        AssignFollowTarget(particles2, actor2);
        AssignParticleValues(particles1, actor1Teleportable);
        AssignParticleValues(particles2, actor1Teleportable);

    }

    void CowMooAudio()
    {
        if (actor1.GetComponent<Teleportable>().isCow || actor2.GetComponent<Teleportable>().isCow)
        {
            if (actor1.GetComponent<Teleportable>().isCow)
            {
                actor1.GetComponent<AudioSource>().Play();
            }
            else if (actor2.GetComponent<Teleportable>().isCow)
            {
                actor2.GetComponent<AudioSource>().Play();
            }
        }
    }

>>>>>>> 1301bd727bb7bb346367bb18e2bc4f319146e3b2
    // Update is called once per frame
    void Update()
    {

    }
}
