using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGrower : MonoBehaviour
{
    public static ShieldGrower Instance { get;private  set; }
    public Transform shieldPartToActivate;
   [SerializeField] GameObject shield;
    [SerializeField] float shieldPartActivationDelay;
    private int shieldPartNo;
    public List<GameObject>activeShieldParts;
    public int ShieldPartNo()
    {
        return shieldPartNo;
    }
    // Start is called before the first frame update
    void Start()
    {
        shieldPartNo = 0;
        activeShieldParts = new List<GameObject>();
        Instance = this;
        LoadShieldParts();

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

     






        foreach (GameObject part in activeShieldParts)
        {

            if (!part.activeInHierarchy)
            {
                Debug.Log(" ---"+part.name);
                part.SetActive(true);
                break;

            }
           
        }
        // shieldPartToActivate.gameObject.SetActive(true);

    }

    private void LoadShieldParts()
    {
       for(int i = 0; i <= 31; i++)
        {
            shieldPartToActivate = shield.transform.GetChild(31 - shieldPartNo);
            activeShieldParts.Add(shieldPartToActivate.gameObject);
            shieldPartNo++;
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
