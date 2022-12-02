using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;


//Derived class
public class MoveHazard : Hazard
{
    [SerializeField] protected Vector3 chestnutMoveLocation;
    [SerializeField] bool lockToVertical;
    [SerializeField] bool lockToHorizontal;
    private Vector3 chestnutSpawnPos;
    
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
            transform.position = Vector3.Lerp(chestnutMoveLocation, chestnutSpawnPos, timer); // lerp 1 pos to other
        }

        else
        {
            transform.position = Vector3.Lerp(chestnutSpawnPos, chestnutMoveLocation, timer); // lerp 1 pos to other
        }
    }


    private void Update()
    {
         Move();
    }
}
