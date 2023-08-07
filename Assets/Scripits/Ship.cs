using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public static Ship Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
