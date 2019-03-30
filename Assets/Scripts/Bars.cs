using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bars : MonoBehaviour
{
    Dictionary<string, Bar> bars = new Dictionary<string, Bar>();
    public GameObject prefab;

    public Vector3 offSet;
    private Vector3 bounding;
    private MeshCollider collide;
    public bool ready = false;

    // Start is called before the first frame update
    void Start() {}

    public void initialize() {
        collide = GetComponent<MeshCollider>();
        bounding = collide.bounds.size / 2;
        ready = true;
    }

    public bool addBar(string name, float initialValue) {
        if (!ready) {
            print(gameObject.name);
            print("Didn't add");
            return false;
        }
        if (bars.ContainsKey(name)) {
            return false;
        } else {
            Bar b = gameObject.AddComponent<Bar>();
            Vector3 pos = gameObject.transform.position + offSet;
            int canValid = 20;
            Vector3 potPos = pos;
            while (canValid > 0) {
                bool canEscape = true;
                potPos = pos + new Vector3(Random.value * bounding.x/3, 0, Random.value * bounding.z/2);
                foreach (Bar x in bars.Values) {
                    Vector3 bPos = x.transform.position;
                    double rScale = x.getRoughScale() * 2;
                    if (potPos.x > bPos.x - rScale && potPos.x < bPos.x + rScale && potPos.z > bPos.z - rScale && potPos.z < bPos.z + rScale) {
                        canEscape = false;
                        break;
                    }
                }
                if (!collide.bounds.Contains(potPos)){
                    canEscape = false;
                }
                if (canEscape) {
                    break;
                }
                canValid--;
            }
            b.initialize(prefab, potPos);
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
    
    public bool isReady() {
        return ready;
    }
}
