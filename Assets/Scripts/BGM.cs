/*  Project: Capstone 3D Escape Room Challenge - Alchemy Room
    Author: Theresa Quach
    Description: BGM is responsible for controlling the fade in and fade out of the background music for the Alchemy Room level of the Escape Room Challenge game.
                This script is attached to the audio source containing the background music for the room.
    Citations:  Code developed by following SwishSwoosh's tutorial on Youtube. (https://www.youtube.com/watch?v=kYGXGDjL5jM)
*/

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        // Set opening music volume to zero to prep for fade in
        bgmSource = GetComponent<AudioSource>();
        bgmSource.volume = 0;
        bgmSource.loop = true;
        bgmSource.Play();
        StartCoroutine(Fade(true, bgmSource, 3f, 0.3f));          // Coroutine for fadein first
        StartCoroutine(Fade(false, bgmSource, 3f, 0f));         // Coroutine for fadeout next
    }

    // Update is called once per frame
    void Update()
    {
        // if no bgm is detected to be playing, bgm begins playing again (loops), including fades
        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
            StartCoroutine(Fade(true, bgmSource, 3f, 0.3f));        // Coroutine for fadein first
            StartCoroutine(Fade(false, bgmSource, 3f, 0f));         // Coroutine for fadeout next
        }
    }


    // Use coroutines to add fade; boolean value controls whether the music is fading in or out
    public IEnumerator Fade (bool fadeIn, AudioSource source, float duration, float targetVol)
    {   // fade out will begin near end of audio source
        if (!fadeIn)
        {
            // calculate length of time of the audio source
            double bgmLength = (double)source.clip.samples / source.clip.frequency;       // Time on line 39 specifies the amount of time taken to finish calculation on line 38
            yield return new WaitForSecondsRealtime((float)(bgmLength - duration));
        }

        // fade in coroutine of music will start immediately
        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVol, time / duration);    // interpolate start to target volume over chosen duration of fade (parameter)
            yield return null;
        }

        yield break;
    }
}
