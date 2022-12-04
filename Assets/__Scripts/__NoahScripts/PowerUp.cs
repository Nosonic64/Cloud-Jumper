using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PowerUp : MonoBehaviour
{
    #region private variables
    private AudioSource audioSource;
    private MeshRenderer mesh;
    #endregion

    #region serialized variables
    [SerializeField] private int id;
    #endregion

    #region getters and setters
    public int Id { get => id;}
    #endregion

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mesh = GetComponentInChildren<MeshRenderer>();    
    }

    public void PowerUpObtained()
    {
        audioSource.Play();
        mesh.enabled = false;
        Invoke("DestroyPowerUp", 2f);
    }

    private void DestroyPowerUp()
    {
        Destroy(gameObject);
    }
}
