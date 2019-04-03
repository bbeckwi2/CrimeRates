using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TimeController : MonoBehaviour
{

    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Boolean backwardAction;
    public SteamVR_Action_Boolean forwardAction;
    public SteamVR_Action_Boolean dateFlash;

    public GameObject masterObject;
    public GameObject textObject;

    private ChartController chart;
    private YearDisplay text;

    [Range(1,1000)]
    public int skipDelay = 100;
    private int skip = 0;

    // Start is called before the first frame update
    void Start() {
        chart = masterObject.GetComponent<ChartController>();
        text = textObject.GetComponent<YearDisplay>();
        text.setText(chart.cDate);
    }

    // Update is called once per frame
    void Update() {
        if (forwardAction.GetState(handType)) {
            if (skip == 0) {
                text.show(chart.forwardYear());
                skip++;
            }
            skip = (skip + 1) % skipDelay;
            
        } else if (backwardAction.GetState(handType)) {
            if (skip == 0) {
                text.show(chart.backwardYear());
                skip++;
            }
            skip = (skip + 1) % skipDelay;
        } else if (dateFlash.GetState(handType)) {
            text.reshow();
        } else {
            skip = 0;
        }
    }
}
