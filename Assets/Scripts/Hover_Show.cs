using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover_Show : MonoBehaviour
{
    public GameObject fontMesh;
    Collider collide;
    GameObject bar;
    public GameObject head;
    int chance = 0;
    int maxChance = 50;
    Collider collideWith;
    Color color;
    Material mat;

    // Start is called before the first frame update
    void Start() {
        collide = fontMesh.GetComponent<BoxCollider>();
        mat = fontMesh.GetComponent<Material>();
        color = Color.white;
    }

    // Update is called once per frame
    void Update() {
        if (bar != null) {
            fontMesh.transform.position = this.transform.position + Vector3.up * 0.05f;
            Vector3 msh = fontMesh.transform.eulerAngles;
            Vector3 ctr = this.transform.rotation.eulerAngles;
            msh.y = ctr.y;
            fontMesh.transform.rotation = Quaternion.Euler(msh);
            color.a = 1.0f;
            fontMesh.GetComponent<MeshRenderer>().material.color = color;
        } else if (chance > 0) {
            chance--;
            color.a = ((float)chance) / ((float)maxChance);
            fontMesh.GetComponent<MeshRenderer>().material.color = color;
        } else {
            fontMesh.transform.position = new Vector3(0f, -1000f, 0f);
        }
        fontMesh.transform.LookAt(head.transform);
    }

    // Draw the label when we put our hand into the bar
    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Bar(Clone)" && bar == null) {
            TextMesh m = fontMesh.GetComponent<TextMesh>();
            Bar b = col.gameObject.GetComponent<Bar>();
            print(b);
            m.text = b.getLabel();
            bar = col.gameObject;
            chance = maxChance;
            collideWith = col;
        }
    }

    // Remove the label when remove our hand
    void OnTriggerExit(Collider col) {
        if (col == collideWith) {
            bar = null;
        }
    }
}
