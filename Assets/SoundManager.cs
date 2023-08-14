using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get;  private set; }
    public Sound[] sounds;

    private void Awake()
    {
        Instance = this;
      foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
          
        }

        Play("backGround");

    }

    public void Play(string name)
    {
        Sound sp=Array.Find(sounds, s => s.soundName == name);
        sp.source.Play();
    }

}
