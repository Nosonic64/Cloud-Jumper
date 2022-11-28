using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameHandler : MonoBehaviour
{
    public GameObject[] thingsToSwitch =  new GameObject[0];
    private int i = 0;
    public KeyCode key;

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
