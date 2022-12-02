using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeapotHazard : Hazard
{
    [SerializeField] Droplet dropletPrefab;
    public Vector3 directionOfObject;


    public float spawnTime;
    float spawnTimer;

    public void SpawnTears()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime)
        {

            Droplet drop = Instantiate(dropletPrefab, transform.position, Quaternion.identity);
            drop.Init(directionOfObject);
            spawnTimer = 0f;
        }
    }

    private void Update()
    {
        SpawnTears();
    }


}
