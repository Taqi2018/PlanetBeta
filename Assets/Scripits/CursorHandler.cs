using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour
{
    [SerializeField] GameObject cursor;

    void Start()
    {
       
        EventGenrator.Instance.OnPlayerWalking += ChangeCursorPosition;

    }

    private void ChangeCursorPosition(object sender, EventGenrator.OnPlayerWalkingEventArgs e)
    {
        cursor.SetActive(true);
        transform.position = e.PlayerWalkToPoint;
    }


}
