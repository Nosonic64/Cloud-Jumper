using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lantern : Hazard
{
    [SerializeField] protected Vector3 moveLocation;
    [SerializeField] bool lockToVertical;
    [SerializeField] bool lockToHorizontal;
    private Vector3 lanternSpawnPos;
    public float destroyPos = 50;
    private Vector3 moveDirection;

    //private float timer;

    public bool goingUp;
    bool isReversing;
    public bool released = false;

    public override void HazardAwake() 
    {
            lanternSpawnPos = transform.position;

            if (lockToVertical)
            {
                moveLocation.x = transform.position.x;
            }
            if (lockToHorizontal)
            {
                moveLocation.y = transform.position.y;
            }
            moveDirection = moveLocation - lanternSpawnPos;
            moveDirection.Normalize();
    }

    public override void Move()
    {
            transform.position -= moveDirection * Time.deltaTime * moveSpeed;   
    }


    private void Update()
    {
        if (released)
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
}