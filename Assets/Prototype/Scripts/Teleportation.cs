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
    public Teleportable actor1Teleportable;
    public Teleportable actor2Teleportable;

    public Animator anim;

    private Material m_instancedMaterial;
    private Material InstancedMaterial
    {
        get { return m_instancedMaterial; }
        set { m_instancedMaterial = value; }
    }

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
    void OnEnabled()
    {
        anim = GetComponent<Animator>();
    }

    private void AssignFollowTarget(GameObject[] objects, Transform target)
    {
        foreach (GameObject obj in objects)
        {
            Debug.Log("assigned.");
            obj.GetComponent<FollowTransform>().target = target;
        }
    }

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
        InstancedMaterial.SetColor("_MainCol", actor1Teleportable.beamColor1);
        InstancedMaterial.SetColor("_GlowCol", actor1Teleportable.beamColor2);
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

    // Update is called once per frame
    void Update()
    {

    }
}
