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
        transform.localScale = new Vector3(currentTexture.width / 1750f, 0,currentTexture.height / 1750f);
        mesh.material.SetTexture("_BaseMap", currentTexture);
    }
}
