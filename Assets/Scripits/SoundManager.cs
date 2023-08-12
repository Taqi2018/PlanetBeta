using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SoundManager 
{

     public enum Sound
     {
          PlayerIdle,
          PlayerMove,
          PlayerAttack, 
          EnemySpawnSound,
          EnemyAttack,
          ShieldGrowerSound,
          ScorpionAttack,
          DroneAttack
          
          
     }

     private static Dictionary<Sound, float> soundTimerDictionary;


     public static void Initialize()
     {
          soundTimerDictionary = new Dictionary<Sound, float>();
          soundTimerDictionary[Sound.PlayerMove] = 0f;
     }

     public static void PlaySound(Sound sound)
     {
          if (CanPlaySound(sound))
          {
               GameObject soundGameObject = new GameObject("Sound");
               AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
               audioSource.PlayOneShot(GetAudioClip(sound));
               
           
          }
     }

     private static bool CanPlaySound(Sound sound)
     {
          switch (sound) 
          {
               default: 
                    return true;
               case Sound.PlayerMove:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                         float lastTimePlayed = soundTimerDictionary[sound];
                         float playerMoveTimeMax = 0.4f;
                         if (lastTimePlayed + playerMoveTimeMax < Time.time)
                         {
                              soundTimerDictionary[sound] = Time.time;
                              return true;
                         }
                         else
                         {
                              return false;
                         }
                    }
                    else
                    {
                         return true;
                    }
                    

          }

     }


     private static AudioClip GetAudioClip(Sound sound)
     {
          foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClips)
          {
               if (soundAudioClip.sound == sound)
               {
                    return soundAudioClip.audioClip;
               }
          }
          Debug.LogError("Sound " + sound + " not found!");
          return null;


     }
}
