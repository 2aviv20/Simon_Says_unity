using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public Sound[] sounds;
    void Awake() {

        //create sound source for each item in sounds
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(SoundManagerEnum name){
        foreach(Sound s in sounds){
            if(s.name == name){
                s.source.Play();
            }
        }
    }

    public void StopSound(SoundManagerEnum name){
        foreach(Sound s in sounds){
            if(s.name == name){
                s.source.Stop();
            }
        }
    }
}
