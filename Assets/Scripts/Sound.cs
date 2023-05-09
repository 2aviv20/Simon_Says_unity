using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Sound
{
    public SoundManagerEnum name;
    public AudioClip clip;
    public float volume;
    public bool loop;
    public AudioSource source;
}
