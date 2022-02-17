using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidToSpawn;
    public float movementSpeed;

    protected void Awake()
    {
        float randomVal = Random.Range(0, 360);
        Vector3 direction = new Vector3(0, 0, randomVal); ;
        transform.Rotate(direction);
    }

    protected void Update()
    {
        transform.position += transform.right * movementSpeed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile" || other.tag == "Player")
        {
            GameManager.instance.updateScore();
            GameObject asteroidInstanceOne = Instantiate(asteroidToSpawn, transform.position, Quaternion.identity);
            GameManager.instance.addAsteroid(asteroidInstanceOne);
            GameObject asteroidInstanceTwo = Instantiate(asteroidToSpawn, transform.position, Quaternion.identity);
            GameManager.instance.addAsteroid(asteroidInstanceTwo);
            GameManager.instance.destroyAsteroid(gameObject);
        }
    }
}
