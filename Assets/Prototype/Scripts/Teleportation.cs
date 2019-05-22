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
    
    public void Initialize() {
        lr.GetComponent<RenderLine>().point1 = actor1;
        lr.GetComponent<RenderLine>().point2 = actor2;
        AssignFollowTarget(particles1, actor1);
        AssignFollowTarget(particles2, actor2);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
