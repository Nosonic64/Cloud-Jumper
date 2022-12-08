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
    protected Transform spawnChunk;
    public bool isSticky = false;
    public bool released = false;
    
    public void SpawnTears()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime && spawnChunk != null)
        {

            Droplet drop = Instantiate(dropletPrefab, spawnChunk);
            drop.transform.position = transform.position;
            drop.Init(-transform.up);
            spawnTimer = 0f;
        }
    }

    public override void HazardAwake()
    {
        spawnChunk = transform.parent;

        if (isSticky)
        {
            transform.parent = null;
        }
    }

    public override void Move() 
    {
        transform.position -= directionOfObject * Time.deltaTime * moveSpeed;
    }

    private void Update()
    {
        if (released)
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
