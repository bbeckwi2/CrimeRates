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
    private BoxCollider collide;
    private int colorIndex = 0;
    private static Color[] colors = new Color[] {
                             new Color(1f, 0f, 0f, 0.85f),
                             new Color(0f, 0f, 1f, 0.85f),
                             new Color(0f, 1f, 0f, 0.85f),
                             new Color(1f, 0.5f, 0.5f, 0.85f),
                             new Color(0.5f, 1f, 0.5f, 0.85f),
                             new Color(0.5f, 0.5f, 1f, 0.85f),
    };

    // Start is called before the first frame update
    void Start() {}

    public void initialize() {
        collide = gameObject.GetComponent<BoxCollider>();
        bounding = collide.bounds.size / 1.5f;
        gameObject.SetActive(true);
    }

    public bool addBar(string name, float initialValue) {
        if (bars.ContainsKey(name)) {
            return false;
        } else {
            Vector3 pos = gameObject.transform.position + offSet;

            int canValid = 20;
            Vector3 potPos = pos;
            while (canValid > 0) {
                bool canEscape = true;
                potPos = new Vector3(((Random.value - 0.5f) * bounding.x) + collide.bounds.center.x, pos.y, ((Random.value - 0.5f) * bounding.z) + collide.bounds.center.z);
                //potPos = pos + new Vector3(Random.value * bounding.x/2 * ((Random.value < .5)? -1f : 1f), 0, Random.value * bounding.z/2 * ((Random.value < .5) ? -1f : 1f));
                foreach (Bar x in bars.Values) {
                    Vector3 bPos = x.transform.position;
                    double rScale = x.getRoughScale() * 2;
                    if (potPos.x > bPos.x - rScale && potPos.x < bPos.x + rScale && potPos.z > bPos.z - rScale && potPos.z < bPos.z + rScale) {
                        canEscape = false;
                        break;
                    }
                }
                
                if (canEscape) {
                    break;
                }
                canValid--;
            }
            GameObject ba = Instantiate(prefab);
            Bar b = ba.AddComponent<Bar>();
            b.bar = ba;
            b.initialize(potPos);
            b.setColor(colors[colorIndex]);
            colorIndex = (colorIndex + 1) % colors.Length;
            bars.Add(name, b);
            b.setLabel(name);

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
