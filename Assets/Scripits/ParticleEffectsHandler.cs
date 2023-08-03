using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectsHandler : MonoBehaviour
{
    [SerializeField] GameObject playerSelectedCircle;


    // Recfactoring needed (Use event to get info about playerSeletectd )
    void Update()
    {
        if (Player.Instance.IsPlayerSelected())
        {
            playerSelectedCircle.SetActive(true);
        }
        else
        {
            playerSelectedCircle.SetActive(false);
        }
        
    }
}
