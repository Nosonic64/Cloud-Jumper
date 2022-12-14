using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeBGArt : MonoBehaviour
{

    [SerializeField] private Texture[] textures = new Texture[0];

    private Renderer mesh;
    private Texture selectedTexture;

    private void OnAwake()
    {
        mesh = GetComponent<Renderer>();
        selectedTexture = textures[Random.Range(0, textures.Length)];
        mesh.material.SetTexture("_BaseMap", selectedTexture);
        transform.localScale = new Vector3(selectedTexture.width / 250, 1, selectedTexture.height / 250);
    }
}
