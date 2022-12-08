using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeapotHazard : Hazard
{
    [SerializeField] Droplet dropletPrefab;
    public Vector3 directionOfObject;
    public float spawnTime;
    float spawnTimer;
    public float destroyPos = -5;
    //
    public bool released = false; // taken from Lantern 
    //
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

    //NOTES - Droplet spawnPos weird. Need to make it seperate to teapot movePos
    //      - starts off and not turning on once player goes through trigger box
    //
    //public override void HazardAwake() 
    //{
        //lanternSpawnPos = transform.position;

        //if (lockToVertical)
        //{
        //    moveLocation.x = transform.position.x;
        //}
        //if (lockToHorizontal)
        //{
        //    moveLocation.y = transform.position.y;
        //}
        //moveDirection = moveLocation - lanternSpawnPos;
        //moveDirection.Normalize();
    //}

    //

    //
    public override void Move() // taken from Lantern
    {
        transform.position -= directionOfObject * Time.deltaTime * moveSpeed;
    }
    //


    private void Update()
    {
        if (released) // just the - if (released) taken from Lantern
        {
            SpawnTears();
            Move();

            if (transform.position.x < destroyPos)
            {
                Destroy(gameObject);
            }
        }
    }



}
