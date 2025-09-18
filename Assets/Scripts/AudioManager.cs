using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//the audio manager manages the audio :D
public class AudioManager : MonoBehaviour
{

    private static AudioManager _instance;   

    public static AudioManager Instance {  get { return _instance; } }

    public AudioSource music;
    public AudioSource sfx;
    public AudioClip[] soundEffects;
    public AudioClip[] soundTracks;


    private void Awake()   //destroy any duplicates whena  new game is started
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;
        }
    }

    void Start()
    {        
        DontDestroyOnLoad(this.gameObject);   //make sure original isnt deleted
    }

    public void PlaySoundEffect(int i)   //plays the sounf fx at whatever index is included in the function call
    {
        sfx.PlayOneShot(soundEffects[i]);
    }
    public void PlaySoundTrack(int i)  //plays the sound track at whatever index is included in the function call
    {
        music.clip = soundTracks[i];
        music.Play();
    }

    public void StopSoundTrack(int i)   //stops the sounf track at whatever index is included in the function call
    {
        music.clip = soundTracks[i];
        music.Stop();
    }


}
