using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
     private static GameAssets _i;


     private void Awake()
     {
          SoundManager.Initialize();
     }

     public static GameAssets i
     {
          get
          {
               if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
               return _i;
          }
     }

     public SoundAudioClip[] soundAudioClips;


     [System.Serializable]
     public class SoundAudioClip
     {
          public SoundManager.Sound sound;
          public AudioClip audioClip;

     }
}
