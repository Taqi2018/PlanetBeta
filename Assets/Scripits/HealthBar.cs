using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider barSlider;

    public float maxHealth;



    // Start is called before the first frame update
    void Start()
    {
        barSlider.maxValue = maxHealth;
        barSlider.value = barSlider.maxValue;

     
   



    }

    public void SetHealthBar(float health)
    {
        barSlider.value = health;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
