using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Derived class
public class MoveHazard : Hazard
{
    [SerializeField] protected Vector3 chestnutMoveLocation;
    [SerializeField] bool lockToVertical;
    [SerializeField] bool lockToHorizontal;
    private Vector3 chestnutSpawnPos;
    private Vector3 moveDirection;
    
    private float timer;
    
    bool isReversing;

    private void Start()
    {
        chestnutSpawnPos = transform.position;
        
        if (lockToVertical)
        {
            chestnutMoveLocation.x = transform.position.x;
        }
        if (lockToHorizontal)
        {
            chestnutMoveLocation.y = transform.position.y;
        }
        moveDirection = chestnutMoveLocation - chestnutSpawnPos;
        moveDirection.Normalize();
    }



    public override void Move()
    {
        timer += Time.deltaTime * moveSpeed;

        if (timer > 1)
        {
            timer = 0;
        isReversing = !isReversing;
        }

        //transform.position -= new Vector3(moveDistance, 0, 0) * Time.deltaTime * moveSpeed;

        if (isReversing)
        {
            transform.position -= moveDirection * Time.deltaTime * moveSpeed;
        }

        else
        {
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
    }


    private void Update()
    {
         Move();
    }
}
