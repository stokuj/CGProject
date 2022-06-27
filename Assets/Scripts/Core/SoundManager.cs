using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class manages sound in game
/// </summary>
public class SoundManager : MonoBehaviour
{
     /**  Sound Manager obj      */
    public static SoundManager instance { get; private set; }
     /**  Sound source      */
    private AudioSource source;

    /// <summary>
    /// Method for  initialization of the sound manager
    /// </summary>


    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
