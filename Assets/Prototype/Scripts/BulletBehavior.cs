using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody rb;
    Vector3 lastFrameVelocity;

    public float speed;
    public Vector3 direction;
    public bool moving;
    public GameObject origin;
    public GameObject ricochetEffect;
    public GameObject destroyEffect;
    int bounces = 0;
    public int maxBounces = 0;
    public float lifetime = 5;
    [Tooltip("Layers to bounce off.")]
    [SerializeField] LayerMask bounceLayers = 0;

    AudioSource audios;

    public void Move()
    {
        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, direction, out hit))
        //{
        //    target = hit.point;
        //}
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.CanBeTeleported(collision.gameObject))
        {
            if (collision.gameObject != origin)
            {
                GameObject target = collision.gameObject;

                //Vector3 targetPosition = target.transform.position;
                //Vector3 originPosition = origin.transform.position;

                //target.transform.position = originPosition;
                //origin.transform.position = targetPosition;

                GameManager.instance.StartTeleportation(origin, target);

                Destroy(gameObject);
            }
            
        }

        if (bounceLayers == (bounceLayers | (1 << collision.gameObject.layer)))
        {
            Vector3 reflectDir = Vector3.Reflect(lastFrameVelocity, collision.contacts[0].normal);
            transform.rotation = Quaternion.LookRotation(reflectDir);
            rb.velocity = reflectDir;

            //Debug.Log("Hit terrain");

            bounces += 1;
            if (ricochetEffect != null)
            {
                GameObject effect = Instantiate(ricochetEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
                Destroy(effect, 1);
            }
            audios.Play();
        }

        if (collision.gameObject.GetComponent<BulletBehavior>())
        {
            if (destroyEffect != null)
            {
                GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1);
            }
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (bounces >= maxBounces)
        {
            Destroy(gameObject);
        }

        lastFrameVelocity = rb.velocity;
        if (moving)
        {
            Move();
        }
    }
}
