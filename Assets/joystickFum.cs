using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickFum : MonoBehaviour
{
    public static joystickFum Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JEnable()
    {
        transform.gameObject.SetActive(true);
    }
    public void JDisable()
    {
        transform.gameObject.SetActive(false);
    }
}
