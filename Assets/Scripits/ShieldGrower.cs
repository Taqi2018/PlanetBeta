using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGrower : MonoBehaviour
{
    public static ShieldGrower Instance { get;private  set; }
    Transform shieldPartToActivate;
   [SerializeField] GameObject shield;
    [SerializeField] float shieldPartActivationDelay;
    private int shieldPartNo;

    public int ShieldPartNo()
    {
        return shieldPartNo;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
   
        StartCoroutine(ShieldPartActivationDelay());
    }


    IEnumerator ShieldPartActivationDelay()
    {
        ActivateShieldPart();
        yield return new WaitForSeconds(shieldPartActivationDelay);



        StartCoroutine(ShieldPartActivationDelay());
    }

    private void ActivateShieldPart()
    {
  
        shieldPartToActivate = shield.transform.GetChild(31 - shieldPartNo);
        shieldPartToActivate.gameObject.SetActive(true);

        shieldPartNo++;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
