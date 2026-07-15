/* this file is the global sound controller for the game. it keeps ONE audio manager alive between scenes
 * sound effects / music is called by an INDEX NUMBER
*/

using System; // general c# system library
using System.Collections; // unity collection
using System.Collections.Generic; // generic collection
using UnityEngine; // main unity engine library
public class AudioManager : MonoBehaviour // manages music and sound effects, keeps ONE instance alive between scenes
{
    private static AudioManager _instance; // stores the single active AudioManager  
    public static AudioManager Instance {  get { return _instance; } } // allows other scripts to access this manager

    public AudioSource music; // AudioSource for background music
    public AudioSource sfx; // AudioSource for sound effects

    public AudioClip[] soundEffects; // Array of sound effects that can be played by index
    public AudioClip[] soundTracks; // Array of music tracks that can be played by index


    private void Awake()   // runs before instance begins
    {
        if (_instance != null && _instance != this) // if another AudioManager already exists...
        {
            Destroy(this.gameObject); // DESTROY IT!!
        } else // else...
        {
            _instance = this; // set this object as the active AudioManager instance
        }
    }

    void Start() // runs once when this object starts
    {        
        DontDestroyOnLoad(this.gameObject);   // keep this manager alive between scene loads
    }

    public void PlaySoundEffect(int i)   // plays the sound effect at whatever index is included in the function call
    {
        sfx.PlayOneShot(soundEffects[i]); // play selected sound effect ONCE
    }
    public void PlaySoundTrack(int i)  // plays the soundtrack at whatever index is included in the function call
    {
        music.clip = soundTracks[i]; // set selected music track
        music.Play(); // play the selected music
    }

    public void StopSoundTrack(int i)   // stops a music track by index
    {
        music.clip = soundTracks[i]; // set selected music track
        music.Stop(); // stop the selected music
    }


}
