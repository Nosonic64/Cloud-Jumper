using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    #region private variables
    private int i = 0;
    #endregion

    #region serialized variables
    [SerializeField] private GameObject[] thingsToSwitch =  new GameObject[0];
    [SerializeField]private KeyCode key;
    #endregion


    private void OnEnable()
    {
        i = 0;
    }
    void Update()
    {
            if(Input.GetKeyDown(key))
            {
                SwitchStuff(); 
            }
    }

    public void SwitchStuff()
    {
        foreach (GameObject objects in thingsToSwitch)
        {
            if (thingsToSwitch[i].activeSelf == true)
            {
                thingsToSwitch[i].SetActive(false);
            }
            else
            {
                thingsToSwitch[i].SetActive(true);
            }
            i++;
        }
    }
}
