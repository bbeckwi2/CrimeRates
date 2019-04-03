using System;
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
    private string[] interests = { "Violent Crime", "Robbery", "Burglary", "Motor Theft" };

    public int smallestDate = 30000;
    public int largestDate = 0;
    private string cDate;
    private int iDate = 1996;
    public float maxHeight = 10f;

    private Dictionary<string, Bars> states;
    // Year, state, crime = amount
    private Dictionary<string, Dictionary<string, Dictionary<string, float>>> crimes;
    void Start() {

        cDate = iDate.ToString();

        crimes = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
        
        states = new Dictionary<string, Bars>();
        reader = new CSVReader("Assets/Crime_Rate.csv");

        foreach (Bars child in GetComponentsInChildren<Bars>()) {
            // If we initialize there are no errors
            child.initialize();
            states.Add(child.name, child);
        }

        // Create the outer two levels year and state
        foreach (string y in reader.data["Year"]) {
            if (!crimes.ContainsKey(y)) {
                int tmp = int.Parse(y);
                smallestDate = Math.Min(smallestDate, tmp);
                largestDate = Math.Max(largestDate, tmp);

                crimes.Add(y, new Dictionary<string, Dictionary<string, float>>());
                foreach (string s in states.Keys) {
                    crimes[y].Add(s, new Dictionary<string, float>());
                }
            }
        }

        // Populate the data 
        for (int i=0; i < reader.data["Year"].Count; i++) {
            foreach (string s in interests) {
                crimes[reader.data["Year"][i]][reader.data["State Name"][i]].Add(s, float.Parse(reader.data[s][i]));
            }
        }

        Dictionary<string, float> scales = new Dictionary<string, float>();

        foreach (string s in interests) {
            float mVal = 0.000000000000000001f;

            Dictionary<string, Dictionary<string, float>> st = crimes[cDate];
            foreach (string sV in st.Keys) {
                mVal = Math.Max(mVal, st[sV][s]);
            }

            scales.Add(s, mVal);
        }

        foreach (String state in states.Keys) {
            Bars bars = states[state];
            foreach (string s in interests) {
                string title = s;
                float value = crimes[cDate][bars.name][s];
                bars.addBar(title, (value / scales[s]) * maxHeight, title + System.Environment.NewLine + "[" + value.ToString("0.###") + "]" + System.Environment.NewLine + state);
            }            
        }
    }

    public void setCurrentYear(int year) {

        print("Setting the year to: " + year);
        if (year < smallestDate || year > largestDate) {
            return;
        }
        setDate(year);
        Dictionary<string, float> scales = new Dictionary<string, float>();

        foreach (string s in interests) {
            float mVal = 0.000000000000000001f;

            Dictionary<string, Dictionary<string, float>> st = crimes[cDate];
            foreach (string sV in st.Keys) {
                mVal = Math.Max(mVal, st[sV][s]);
            }

            scales.Add(s, mVal);
        }

        foreach (String state in states.Keys) {
            Bars bars = states[state];
            foreach (string s in interests) {
                string title = s;
                float value = crimes[cDate][bars.name][s];
                bars.setGoal(title, (value / scales[s]) * maxHeight);
                bars.setLabel(title, title + System.Environment.NewLine + "[" + value.ToString("0.###") + "]" + System.Environment.NewLine + state);
            }
        }
    }

    private void setDate(int date) {
        iDate = date;
        cDate = iDate.ToString();
    }

    public string forwardYear() {
        if (iDate < largestDate - 1) {
            setCurrentYear(iDate + 1);
        }
        return cDate;
    }

    public string backwardYear() {
        if (iDate > smallestDate + 1) {
            setCurrentYear(iDate - 1);
        }
        return cDate;
    }


    // Update is called once per frame
    void Update() {
    }
}
