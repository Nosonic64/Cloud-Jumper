using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // This script controls basic platforms and how they function.
    #region private variables
    private BoxCollider platCollider;
    private AudioSource audioSource;
    private GameObject player;
    private MeshRenderer mesh;
    private bool playerTouched;
    private float disableTimerCounter;
    private float deleteThreshold = -2.8f;
    private float angleLimitAdd = 0.75f;
    #endregion

    #region serialized variables
    [SerializeField] private float disableTimerSet;
    [SerializeField] private float disappearingEffectRate;
    #endregion

    private void Start()
    {
        platCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponentInChildren<MeshRenderer>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        var meshTransform = GetComponentInChildren<Transform>();
        if (Random.Range(0, 2) == 1)
        {
            meshTransform.localScale = new Vector3(meshTransform.localScale.x * -1, meshTransform.localScale.y, meshTransform.localScale.z);
        }
    }

    private void Update()
    {
        // If the player Y + an offset is over the platform, we make it solid.
        // We need an offset otherwise the player could get stuck in the platform.
        if (GameManager.instance.player.transform.position.y - 0.5f > transform.position.y) 
        {
            platCollider.enabled = true;
        }
        else if(GameManager.instance.player.transform.position.y + 0.5f < transform.position.y)
        {
            platCollider.enabled = false;
        }

        if (!GameManager.instance.levelChunkManager.DontBreakPlats) //Debug: if this bool is on the platforms wont break
        {
            //TODO: Change the timer to go up instead of down as its better practice (and it should eliminate the need for the "playerTouched" boolean)
            if (transform.position.y < deleteThreshold || disableTimerCounter <= 0f && playerTouched) //Start of deletion code, we want to delete the platform if its under Y a certain amount or its timer is 0;
            {
                if(player != null)
                {
                    player.transform.SetParent(null);
                }
                platCollider.enabled = false;
                mesh.transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f) * Time.deltaTime;
            }
        }

        if(disableTimerCounter > 0f)
        {
            disableTimerCounter -= Time.deltaTime;
            angleLimitAdd += disappearingEffectRate * Time.deltaTime;
            mesh.material.SetFloat("_angleLimit", angleLimitAdd);
            mesh.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f) * Time.deltaTime;
        }

        if(mesh.transform.localScale.y <= 0 || mesh.transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) //On collision with the player, we play a sound and set a timer.
        {
            player = collision.gameObject;
            audioSource.Play();
            if (disableTimerCounter <= 0)
            {
                disableTimerCounter = disableTimerSet;
                playerTouched = true;
            }
        }
    }

    private void OnCollisionExit()
    {
        player = null;
    }

}
