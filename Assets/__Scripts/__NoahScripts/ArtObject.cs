using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtObject : MonoBehaviour
{
    [SerializeField] private Texture[] textures = new Texture[0];
    private MeshRenderer mesh;
    private Texture currentTexture;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        currentTexture = textures[Random.Range(0, textures.Length)];
        transform.localScale = new Vector3(currentTexture.width / 1920f, 1,currentTexture.height / 1920f);
        mesh.material.SetTexture("_BaseMap", currentTexture);
        //1750f
    }
}
