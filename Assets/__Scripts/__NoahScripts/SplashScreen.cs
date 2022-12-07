using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{ 
    public void SwitchSplashOn()
    {
        gameObject.SetActive(true);
    }

    public void SwitchSplashOff()
    {
        gameObject.SetActive(false);
    }
}
