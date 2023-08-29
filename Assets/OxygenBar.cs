using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OxygenBar : MonoBehaviour
{


    
    [SerializeField] Slider oxygenSlider;




    // Start is called before the first frame update
    void Start()
    {

    

        oxygenSlider.maxValue = 100;
        oxygenSlider.value = 100;



    }




    public void SetOxygenBar(float health)
    {
        oxygenSlider.value = health;
    }


}
