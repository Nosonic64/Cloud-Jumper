using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float scrollingSpeed;
    public float scollingMultiplier = 1f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Vector3.down * scrollingSpeed * Time.deltaTime * scollingMultiplier; //time.deltatime smooths movement per frame // scrollingMultiplier in other script
    }
}
