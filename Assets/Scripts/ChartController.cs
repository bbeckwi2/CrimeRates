﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartController : MonoBehaviour
{
    // Our bar controller class. 
    // Currently it's just used to change the bar colors randomly
    // It will later be used to orchestrate the bar movements
    public Material normal;
    public Material swap;
    public GameObject prefab;

    private Dictionary<string, Bars> states;

    void Start() {
        states = new Dictionary<string, Bars>();

        foreach (Bars child in GetComponentsInChildren<Bars>()) {
            // If we initialize there are no errors
            child.initialize(); 
            states.Add(child.name, child);
        }

        foreach (Bars b in states.Values) {
            b.addBar("Test", Random.value * 10f);
            b.addBar("Testing", Random.value * 10f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Random.value < 0.0005) {
            foreach (Bars b in states.Values) {
                b.setGoal("Test", Random.value * 10f);
            }
        }
    }
}
