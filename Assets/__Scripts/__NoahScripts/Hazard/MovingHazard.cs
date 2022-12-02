using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Derived class
public class MovingHazard : Hazard
{
    [SerializeField] int damage;
    // Start is called before the first frame update
    //public override void Move()
    //{
    //    Debug.Log("Moving Hazard around Up and Down");
    //}

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //Deal damage to the player
        }
    }
}
