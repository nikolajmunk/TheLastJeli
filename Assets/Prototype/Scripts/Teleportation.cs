using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform actor1, actor2;
    public LineRenderer lr;
    public GameObject[] particles1, particles2;
    public GameObject spark1;
    public GameObject spark2;
    public GameObject bob1;
    public GameObject bob2;
    public GameObject fresnel1;
    public GameObject fresnel2;
    public Teleportable actor1Teleportable, actor2Teleportable;
    public float actor1Scale, actor2Scale;
    Vector3 actor1OriginalScale, actor2OriginalScale;

    public Animator anim;

    private Material m_instancedMaterial;
    private Material InstancedMaterial
    {
        get { return m_instancedMaterial; }
        set { m_instancedMaterial = value; }
    }

    private Material FresnelMaterial1
    {
        get { return m_instancedMaterial; }
        set { m_instancedMaterial = value; }
    }

    private Material FresnelMaterial2
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

    void AssignSparkColor(GameObject spark, Teleportable actorTeleportable)
    {
        ParticleSystem ps;
        ps = spark.GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule col = ps.colorOverLifetime;
        col.color = actorTeleportable.sparkGradient;
    }

    void AssignBobColor(GameObject bob, Teleportable actorTeleportable)
    {
        ParticleSystem ps;
        ps = bob.GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule col = ps.colorOverLifetime;
        col.color = actorTeleportable.bobGradient;
    }

    void AssignFresnelColors(GameObject fresnel, Material fresnelMaterial, Teleportable actorTeleportable)
    {
        ParticleSystemRenderer ps = fresnel.GetComponent<ParticleSystemRenderer>();
        fresnelMaterial = ps.material;
        fresnelMaterial.SetColor("_Color1", actor1Teleportable.beamColor1);
        fresnelMaterial.SetColor("_Color2", actor1Teleportable.beamColor2);
        ps.material = fresnelMaterial;
    }

    public void ScaleAround(Transform target, Vector3 pivot, Vector3 newScale)
    {
        Vector3 A = target.localPosition;
        Vector3 B = pivot;

        Vector3 C = A - B; // diff from object pivot to desired pivot/origin

        float RS = newScale.x / target.localScale.x; // relataive scale factor

        // calc final position post-scale
        Vector3 FP = B + C * RS;    

        // finally, actually perform the scale/translation
        target.localScale = newScale;
        target.localPosition = FP;
    }

    public void CustomPivotScale(Transform target, Vector3 pivot, Vector3 newScale)
    {
        Vector3 diff = target.localPosition - pivot;
        target.localPosition = pivot - diff * newScale.x;
        target.localScale = newScale;
    }

    public void Initialize()
    {
        lr.GetComponent<RenderLine>().point1 = actor1;
        lr.GetComponent<RenderLine>().point2 = actor2;

        actor1Teleportable = actor1.GetComponent<Teleportable>();
        actor2Teleportable = actor2.GetComponent<Teleportable>();

        actor1OriginalScale = actor1.Find("Model").localScale;
        actor2OriginalScale = actor2.Find("Model").localScale;

        InstancedMaterial = lr.material;
        InstancedMaterial.SetColor("_MainCol", actor1Teleportable.beamColor1);
        InstancedMaterial.SetColor("_GlowCol", actor1Teleportable.beamColor2);
        lr.material = InstancedMaterial;
        AssignFollowTarget(particles1, actor1);
        AssignFollowTarget(particles2, actor2);
        AssignParticleValues(particles1, actor1Teleportable);
        AssignParticleValues(particles2, actor1Teleportable);
        AssignSparkColor(spark1, actor1Teleportable);
        AssignSparkColor(spark2, actor1Teleportable);
        AssignBobColor(bob1, actor1Teleportable);
        AssignBobColor(bob2, actor1Teleportable);
        AssignFresnelColors(fresnel1, FresnelMaterial1, actor1Teleportable);
        AssignFresnelColors(fresnel2, FresnelMaterial2, actor1Teleportable);
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
        //CustomPivotScale(actor1.Find("Model"), actor1.Find("ScalePivot").localPosition, actor1OriginalScale * actor1Scale);
        //CustomPivotScale(actor2.Find("Model"), actor2.Find("ScalePivot").localPosition, actor2OriginalScale * actor1Scale);
        //actor1.localScale = actor1OriginalScale * actor1Scale;
        //actor2.localScale = actor2OriginalScale * actor2Scale;
    }
}
