using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Small : Asteroid
{
    private void Start()
    {

    }



    public override void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Projectile")
        {
            GameManager.instance.updateScore();
            GameManager.instance.destroyAsteroid(gameObject);
        }
    }
}
