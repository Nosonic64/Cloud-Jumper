using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Hazard
{
    [SerializeField] protected Vector3 chestnutMoveLocation;
    [SerializeField] bool lockToVertical;
    [SerializeField] bool lockToHorizontal;
    private Vector3 lanternSpawnPos;
    public float destroyPos = 50;

    private float timer;

    public bool goingUp;
    bool isReversing;

    private void Start()
    {
        lanternSpawnPos = transform.position;

        if (lockToVertical)
        {
            chestnutMoveLocation.x = transform.position.x;
        }
        if (lockToHorizontal)
        {
            chestnutMoveLocation.y = transform.position.y;
        }
    }

    

    public override void Move()
    {
        timer += Time.deltaTime * moveSpeed;

        if (timer > 1)
        {
            timer = 0;
            isReversing = !isReversing;
        }

        if (isReversing)
        {
            transform.position = Vector3.Lerp(chestnutMoveLocation, lanternSpawnPos, timer); // lerp 1 pos to other
        }

        else
        {
            transform.position = Vector3.Lerp(lanternSpawnPos, chestnutMoveLocation, timer); // lerp 1 pos to other
        }
    }

    
    
    // check if can go in movehazard script instead


    private void Update()
    {
        Move();
        
        if (goingUp) 
        {
            if (transform.position.y > destroyPos)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (transform.position.y < destroyPos)
            {
                Destroy(gameObject);
            }
        }
    }
}