using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiedDestructionEvent : MonoBehaviour
{
    public static ShiedDestructionEvent Instance { private set; get; }
    public event EventHandler OnDestructionOfLastPart;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame


    private void OnDisable()
    {
        OnDestructionOfLastPart?.Invoke(this, EventArgs.Empty);
    }
}
