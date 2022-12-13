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
            Lantern lantern = releaseObject.GetComponent<Lantern>();
            TeapotHazard teapot = releaseObject.GetComponent<TeapotHazard>();

            if (lantern != null)
            {
                releaseObject.SetActive(true);
                lantern.released = true;
            }
            if (teapot != null)
            {
                releaseObject.SetActive(true);
                teapot.released = true;
            }
            Destroy(gameObject);
        }
    }

}
