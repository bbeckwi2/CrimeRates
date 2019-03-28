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

    private List<Bars> states;

    void Start() {
        states = new List<Bars>();
        foreach (Bars child in GetComponentsInChildren<Bars>()) {
            states.Add(child);
        }

        print(states.Count);

        foreach (Bars b in states) {
            print(b);
            b.addBar("Test", Random.value * 10f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Random.value < 0.0005) {
            foreach (Bars b in states) {
                b.setGoal("Test", Random.value * 10f);
            }
        }
    }
}
