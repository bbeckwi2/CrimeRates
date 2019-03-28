using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bars : MonoBehaviour
{
    Dictionary<string, Bar> bars = new Dictionary<string, Bar>();
    public GameObject prefab;

    public Vector3 offSet; 

    // Start is called before the first frame update
    void Start() {
    }

    public bool addBar(string name, float initialValue) {
        if (bars.ContainsKey(name)) {
            return false;
        } else {
            Bar b = gameObject.AddComponent<Bar>();
            b.initialize(prefab, gameObject.transform.position + offSet);
            bars.Add(name, b);
            this.setGoal(name, initialValue);
            return true;
        }
    }

    public bool setGoal(string name, float newValue) {
        if (bars.ContainsKey(name)) {
            bars[name].SetGoal(newValue);
            return true;
        } else {
            return false;
        }
    }

    public bool deleteBar(string name) {
        if (bars.ContainsKey(name)) {
            bars.Remove(name);
            return true;
        } else {
            return false;
        }
    }

    public string[] getBarList(string name) {
        return bars.Keys.ToArray<string>();
    }
    
}
