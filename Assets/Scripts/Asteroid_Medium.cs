using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Medium : Asteroid
{
    public GameObject smallAsteroidPref;
    private void Start()
    {
        asteroidToSpawn = smallAsteroidPref;
    }
}