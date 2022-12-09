using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerUp : MonoBehaviour
{
    #region private variables
    private AudioSource audioSource;
    private AudioClip clip;
    private ParticleSystem myParticle;
    private MeshRenderer mesh;
    private Animator anim;
    #endregion

    #region serialized variables
    [SerializeField] private int id;
    #endregion

    #region getters and setters
    public int Id { get => id;}
    public AudioClip Clip { get => clip;}
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myParticle = GetComponentInChildren<ParticleSystem>();
        mesh = GetComponentInChildren<MeshRenderer>();
        anim = GetComponent<Animator>();
        clip = audioSource.clip;
    }

    private void Update()
    {
        if(transform.position.y <= -2.8f)
        {
            DestroyPowerUp();
        }
    }

    public void PowerUpObtained()
    {
        if (GameManager.instance.powerUpManager.CurrentPlayingPowerUpClip != clip) 
        {
            GameManager.instance.powerUpManager.CurrentPlayingPowerUpClip = clip;
            audioSource.Play();
        }
        anim.SetBool("collected", true);
        myParticle.Stop();
        Invoke("DestroyPowerUp", 2.4f);
    }

    private void DestroyPowerUp()
    {
        Destroy(gameObject);
    }

    private void TurnOffMesh()
    {
        mesh.enabled = false;
    }
}
