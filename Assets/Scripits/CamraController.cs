using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraController : MonoBehaviour
{
    

    public GameObject Player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 clipXaxis = new Vector3(0, offset.y, 0);
        Vector3 playerClipXaxis= new Vector3(0, 0, Player.transform.position.z);
        transform.position = playerClipXaxis + clipXaxis;
    }

}
