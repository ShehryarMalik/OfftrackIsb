using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 25;
    [SerializeField] private GameObject bulletDecal;
    [SerializeField] private float speed = 50f, timeToDestroy = 3f;
    [SerializeField] private ParticleSystem bulletImpact1;

    public Vector3 target { get; set;}
    public bool hit { get; set; }



    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        // + contact.normal * 0.0001f Quaternion.LookRotation(contact.normal) * Quaternion.Euler(-180f,0,0f))
        GameObject.Instantiate(bulletDecal, contact.point, Quaternion.FromToRotation(Vector3.forward, contact.normal)) ;
        Destroy(gameObject);

        ShootableObject otherShootable = other.gameObject.GetComponent<ShootableObject>();
        
        if(otherShootable != null)
        {
            otherShootable.ShotReaction(bulletDamage);
        }
    }
}
