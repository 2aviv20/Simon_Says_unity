using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public Sound[] sounds;
    // Start is called before the first frame update
    void Awake() {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
       
    }

    public void PlaySound(ButtonColorsEnum name){
        foreach(Sound s in sounds){
            if(s.name == name){
                // if(!s.source.isPlaying){
                    s.source.Play();
                // }
            }
        }
    }

    public void StopSound(ButtonColorsEnum name){
        foreach(Sound s in sounds){
            if(s.name == name){
                s.source.Stop();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
