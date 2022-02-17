using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saucer : MonoBehaviour
{
    public float movementSpeed;
    public GameObject saucerProjectile;
    public Vector3 direction;
    public Transform target;

    protected void Start()
    {
        InvokeRepeating("shootPlayer", 2f, 2f);
        InvokeRepeating("newDirection", 0f, 3f);
    }


    protected void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direction, movementSpeed * Time.deltaTime);
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" || other.tag == "Player")
        {
            GameManager.instance.updateScore();
            GameManager.instance.destroySaucer(gameObject);
        }
    }


    public void shootPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject instance = Instantiate(saucerProjectile, transform.position, Quaternion.identity);
        instance.transform.rotation = Quaternion.LookRotation(target.position, Vector3.up);
    }

    public void newDirection()
    {
        float spawnY = Random.Range
                (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        float spawnX = Random.Range
            (Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

        direction = new Vector3(spawnX, spawnY, 0);
    }
}
