using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
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

    public void SwitchStuff() //Switches things from enabled to disables, or disabled to enabled depending on its current status.
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
