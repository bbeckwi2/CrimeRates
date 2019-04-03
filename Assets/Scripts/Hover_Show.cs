using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover_Show : MonoBehaviour
{
    public GameObject fontMesh;

    public GameObject head;
    public int fadeDelay = 50;

    [Range(1,1000)]
    int fade = 0;

    private Collider collide;
    private GameObject bar;
    private Collider collideWith;
    private Color color;

    // Start is called before the first frame update
    void Start() {
        collide = fontMesh.GetComponent<BoxCollider>();
        color = Color.white;
    }

    // Update is called once per frame
    void Update() {
        if (bar != null) {
            fontMesh.transform.position = this.transform.position + Vector3.up * 0.05f;
            color.a = 1.0f;
            fontMesh.GetComponent<MeshRenderer>().material.color = color;
        } else if (fade > 0) {
            fade--;
            color.a = ((float)fade) / ((float)fadeDelay);
            fontMesh.transform.position = this.transform.position + Vector3.up * 0.05f;
            fontMesh.GetComponent<MeshRenderer>().material.color = color;
        } else {
            fontMesh.transform.position = new Vector3(0f, -1000f, 0f);
        }
        fontMesh.transform.LookAt(head.transform);
    }

    // Draw the label when we put our hand into the bar
    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.name == "Bar(Clone)" && bar == null) {
            TextMesh mesh = fontMesh.GetComponent<TextMesh>();
            Bar b = col.gameObject.GetComponent<Bar>();
            mesh.text = b.getLabel();
            bar = col.gameObject;
            fade = fadeDelay;
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
