using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBooster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player)){
            player.oxygen = 100;
            player.playerOxBar.SetOxygenBar(100);
        }
    }
}
