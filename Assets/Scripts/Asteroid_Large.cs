using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Large : Asteroid
{
    public GameObject mediumAsteroidPref;

    private void Start()
    {
        asteroidToSpawn = mediumAsteroidPref;
    }


}
