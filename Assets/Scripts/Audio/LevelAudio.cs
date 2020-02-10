using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAudio : MonoBehaviour
{
    private static AudioClip waterSplash, punch;
    private static AudioSource source;

    void Start()
    {
        //Find audio clip in resource folder
        waterSplash = Resources.Load<AudioClip>("Audio/watersplash");
        punch = Resources.Load<AudioClip>("Audio/punch");

        source = GetComponent<AudioSource>();
    }

    public static void PlayAudio(string audioTitle) {

        switch (audioTitle)
        {
            case "watersplash":
                //plays clip (PlayOneShot can play multiple clips)
                source.PlayOneShot(waterSplash);
                break;

            case "punch":
                source.PlayOneShot(punch);
                break;

            default:
                break;
        }
    }
}
