using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitLifeChange : MonoBehaviour
{
    // This script handles changing the material on the mesh of
    // the butterfly that follows the fox.
    // (Kit is the name of it)

    // Calling up the ChangeMat function is handled in PlayerMovement,
    // whenever the player loses a life.
    #region private variables
    private SkinnedMeshRenderer[] mesh = new SkinnedMeshRenderer[0];
    #endregion

    #region serialized variables
    [SerializeField] private Material[] materials = new Material[0];
    #endregion

    private void Start()
    {
        mesh = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public void ChangeMat(int materialToChangeTo)
    {
        // Kit is made up of multiple meshes, each having a different slot for materials.
        // We must go through every SkinnedMeshRenderer on the object this script is attached to
        // in order to change the materials for each.
        foreach (SkinnedMeshRenderer skin in mesh)
        {
           skin.material = materials[materialToChangeTo];
        }
    }
}
