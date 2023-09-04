using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private AudioSource audioSource;
    private const string MUSIC_PREF_KEY = "MusicMuted";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateVolume();
    }

    public void ToggleMute()
    {
        int isMuted = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 0);
        isMuted = 1 - isMuted; // toggles between 1 and 0
        PlayerPrefs.SetInt(MUSIC_PREF_KEY, isMuted);
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        int isMuted = PlayerPrefs.GetInt(MUSIC_PREF_KEY, 0);
        audioSource.volume = (isMuted == 1) ? 0 : 1;
    }
}
