using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseObject : MonoBehaviour
{
     public GameObject releaseObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var temp = releaseObject.GetComponent<Lantern>();
            //var temp = releaseObject.GetComponent<TeapotHazard>(); What do I use other than temp?

            if (temp != null)
            {
                releaseObject.SetActive(true);
                temp.released = true;
            }
        }
    }

}
