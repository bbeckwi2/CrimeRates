using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TimeController : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean backwardAction;
    public SteamVR_Action_Boolean forwardAction;
    public GameObject masterObject;
    private ChartController chart;
    public int skipDelay = 100;
    private int skip = 0;

    // Start is called before the first frame update
    void Start() {
        chart = masterObject.GetComponent<ChartController>();
    }

    // Update is called once per frame
    void Update() {
        if (forwardAction.GetState(handType)) {
            if (skip == 0) {
                chart.forwardYear();
                skip++;
            }
            skip = (skip + 1) % skipDelay;
            
        } else if (backwardAction.GetState(handType)) {
            if (skip == 0) {
                chart.backwardYear();
            }
            skip = (skip + 1) % skipDelay;
            
        } else {
            skip = 0;
        }
    }
}
