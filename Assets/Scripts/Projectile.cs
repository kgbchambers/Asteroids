using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

}
