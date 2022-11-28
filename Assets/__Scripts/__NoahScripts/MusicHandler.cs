using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource[] audioSources;
    public AudioClip[] clips = new AudioClip[0];

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    private void Update()
    {
        if (!audioSources[0].isPlaying && audioSources[0].clip != null)
        {
            MusicFadeInCall();
            //audioSources[0].clip = null;
        }
    }

    public void PlayPowerUpMusic(int i)
    {
        audioSources[0].clip = clips[i];
        audioSources[0].Play();
    }

    public void MusicFadeOutCall()
    {
        StopAllCoroutines();
        StartCoroutine(MusicFadeOut(0f));
    }

    public void MusicFadeInCall()
    {
        StartCoroutine(MusicFadeIn(1f));
    }

    public IEnumerator MusicFadeOut(float targetVolume)
    {
        while (true)
        {
            while (audioSources[1].volume > targetVolume)
            {
                audioSources[1].volume -= Time.deltaTime;
                yield return new WaitForSeconds(.005f);
            }
            yield return null;
        }
    }

    public IEnumerator MusicFadeIn(float targetVolume)
    {
        while (true)
        {
            while (audioSources[1].volume < targetVolume)
            {
                audioSources[1].volume += Time.deltaTime;
                yield return new WaitForSeconds(.005f);
            }
            yield return null; 
        }
    }
}
