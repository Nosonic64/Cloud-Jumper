using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitLifeChange : MonoBehaviour
{
    [SerializeField] private Material[] materials = new Material[0];
    private SkinnedMeshRenderer[] mesh = new SkinnedMeshRenderer[0];

    private void Start()
    {
        mesh = GetComponents<SkinnedMeshRenderer>();
        mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void ChangeMat(int materialToChangeTo)
    {
        foreach (SkinnedMeshRenderer go in mesh)
        {
           go.material = materials[materialToChangeTo];
        }
    }
}
