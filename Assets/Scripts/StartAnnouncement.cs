using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnnouncement : MonoBehaviour
{
    AudioSource launchPadAudioSource;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Start Announcement");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            launchPadAudioSource = GetComponent<AudioSource>();

            if(!launchPadAudioSource.isPlaying)
            {
                launchPadAudioSource.Play();
            }
        }
    }
}
