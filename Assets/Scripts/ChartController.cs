using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartController : MonoBehaviour
{
    // Our bar controller class. 
    // Currently it's just used to change the bar colors randomly
    // It will later be used to orchestrate the bar movements
    public Material normal;
    public Material swap;


    // Update is called once per frame
    void Update()
    {
        
        if (Random.value < 0.0025) {
            Bar b;
            foreach (Bar child in GetComponentsInChildren<Bar>()) {
                if (Random.value < 0.25) {
                    child.bar.GetComponent<Renderer>().material = swap;
                    //child.material = swap;
                } else {
                    child.bar.GetComponent<Renderer>().material = normal;
                    //child.material = normal;
                }
            }
        }
    }
}
