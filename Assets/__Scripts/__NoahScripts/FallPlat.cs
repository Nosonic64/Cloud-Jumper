using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlat : MonoBehaviour
{
    public float disableTimer;
    public float disableTimerSet;
    public bool playerTouched;
    private BoxCollider collider;
    private MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        disableTimer = disableTimerSet;
    }


    void Update()
    {
        if (disableTimer > 0f)
        {
            disableTimer -= 1f * Time.deltaTime;
            float lerp = Mathf.PingPong(Time.time, disableTimer) / disableTimer;
            mesh.material.color = Color.Lerp(Color.yellow, Color.black, lerp);
        }

        if (playerTouched && !PlayerInfo.playerGrounded && disableTimer <= 5f || disableTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerTouched = true;
        }
    }
}
