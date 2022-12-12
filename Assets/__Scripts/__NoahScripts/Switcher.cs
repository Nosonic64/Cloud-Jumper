using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    // This script acts like a switch for objects placed in the array.
    // If they are active, we disable them.
    // If they are disabled, we activate them.
    #region serialized variables
    [SerializeField] private GameObject[] thingsToSwitch =  new GameObject[0];
    [SerializeField]private KeyCode key;
    #endregion

    void Update()
    {
            if(Input.GetKeyDown(key))
            {
                SwitchStuff(); 
            }
    }

    public void SwitchStuff() //For each object in the array, we switch them.
    {
        foreach (GameObject go in thingsToSwitch)
        {
            if (go.activeSelf == true)
            {
                go.SetActive(false);
            }
            else
            {
                go.SetActive(true);
            }
        }
    }
}
