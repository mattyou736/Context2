using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    public AudioSource[] musicTracks;

    public int currentTrack;
    public float timeClip;
    public bool musicCanPlay;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       /* if (musicCanPlay)
        {
            
            if (!musicTracks[currentTrack].isPlaying)
            {
                musicTracks[currentTrack].volume = 0.5f;
                
                musicTracks[currentTrack].Play();
            }
            timeClip = musicTracks[currentTrack].time;
        }
        else
        {
            musicTracks[currentTrack].Stop();
        }*/
    }

    public void SwitchTrack(int newTrack)
    {
        musicTracks[currentTrack].Stop();
        currentTrack = newTrack;
        musicTracks[currentTrack].volume = 0.5f;
        musicTracks[currentTrack].time = timeClip;
        musicTracks[currentTrack].Play();
    }
}
