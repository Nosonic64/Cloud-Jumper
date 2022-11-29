using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider collider;
    public float disableTimer;
    public float disableTimerSet;
    private MeshRenderer mesh;
    public bool playerTouched;
    public bool white;
    public bool grey;
    private Color endColor;

    private void Start()
    {
        mesh = GetComponentInChildren<MeshRenderer>();    
        collider = GetComponent<BoxCollider>();
        if (white)
        {
            endColor = Color.white;
        } else if (grey)
        {
            endColor = Color.grey;
        }

        if (ScoreHandler.distance < 30f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        else if(ScoreHandler.distance < 60f)
        {
            transform.localScale = new Vector3(0.85f, 1f, 1f);
        }
        else if (ScoreHandler.distance < 90f)
        {
            transform.localScale = new Vector3(0.65f, 1f, 1f);
        }

    }

    private void Update()
    {
        if(PlayerInfo.playerY - 1f > transform.position.y)
        {
            collider.enabled = true;
        }
        else if(PlayerInfo.playerY < transform.position.y)
        {
            collider.enabled = false;
        }

        if(transform.position.y < -1f)
        {
            Destroy(gameObject);
        }

        if(disableTimer > 0f)
        {
            disableTimer -= 1f * Time.deltaTime;
            float lerp = Mathf.PingPong(Time.time, disableTimer) / disableTimer;
            mesh.material.color = Color.Lerp(Color.black, endColor, lerp);

        }
        else if(disableTimer <= 0f && playerTouched)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" & disableTimer <= 0)
        {
            disableTimer = disableTimerSet;
            playerTouched = true;
        }
    }

}
