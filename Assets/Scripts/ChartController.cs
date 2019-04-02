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
    public GameObject prefab;
    public CSVReader reader;
    private Dictionary<string, Bars> states;

    void Start() {

        states = new Dictionary<string, Bars>();
        reader = new CSVReader("Assets/Crime_Rate.csv");
        foreach(string c in reader.categories) {
            print(c);
        }
        foreach (Bars child in GetComponentsInChildren<Bars>()) {
            // If we initialize there are no errors
            child.initialize(); 
            states.Add(child.name, child);
        }

        foreach (Bars b in states.Values) {
            b.addBar("Test", Random.value * 10f);
            b.addBar("Testing", Random.value * 10f);
            b.addBar("AnotherTest", Random.value * 10f);
            b.addBar("OneMore", Random.value * 10f);
            b.addBar("MaybeTwo", Random.value * 10f);
            b.addBar("AhThree", Random.value * 10f);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Random.value < 0.0005) {
            foreach (Bars b in states.Values) {
                b.setGoal("Test", Random.value * 10f);
                b.setGoal("Testing", Random.value * 10f);
                b.setGoal("AnotherTest", Random.value * 10f);
                b.setGoal("OneMore", Random.value * 10f);
                b.setGoal("MaybeTwo", Random.value * 10f);
                b.setGoal("AhThree", Random.value * 10f);
            }
        }
    }
}
