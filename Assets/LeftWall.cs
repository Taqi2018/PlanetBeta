using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWall : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        arrow.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy e))
        {

            arrow.gameObject.SetActive(true);
            Debug.Log("dete");
            Vector3 raw = other.transform.position - arrow.transform.position;



            Vector3 arrowVector = new Vector3(raw.x, -150, raw.z);
            arrow.transform.forward = Vector3.Slerp(new Vector3(arrow.transform.forward.x, -150, arrow.transform.forward.z), arrowVector, 4.0f);
            StartCoroutine(DisableArrow());
        }
    }
    IEnumerator DisableArrow()
    {
        
        yield return new WaitForSeconds(1);
        arrow.gameObject.SetActive(false);
    }
}
