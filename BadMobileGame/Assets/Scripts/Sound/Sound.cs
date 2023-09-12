using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Each sound in the game is represented as a 'Sound Object'- a scriptable object where you can specify the 
//default values of a sound, like its name, base volume/pitch, looping, and audio mixer group. 
[CreateAssetMenu(fileName = "New Sound", menuName = "Music and Sounds/Sound Object")]
public class Sound : ScriptableObject
{
    public string soundName; // The name of the sound to be called in any of the SoundController functions.

    public AudioClip clip; // The clip itself (.wav, .mp3, etc.) 

    [Range(0, 3f)]
    public float baseVolume = 1; // Base volume of the clip (can be lowered by fade in/out, but this value will not change.)
    [Range(0.2f, 3f)]
    public float basePitch = 1; // Base pitch of this sound. 

    public bool loop; // Should this sound loop itself? 

    public AudioMixerGroup audioMixerGroup; // Useful if you'd like to hook your sound system up to settings, indicates Music vs. SFX etc.

    [HideInInspector]
    public AudioSource source; // AudioSource associated with this sound.
}