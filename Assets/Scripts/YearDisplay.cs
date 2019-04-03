using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YearDisplay : MonoBehaviour
{

    public GameObject fontMesh;
    public GameObject head;

    [Range(1,1000)]
    public int fadeDelay;

    private int fade = 0;
    private bool dir = false;
    private Material mat;
    private Color color = Color.white;


    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        if (dir == true && fade < fadeDelay) {
            fade++;
            updateFontMesh();
        } else if (dir == false && fade > 0) {
            fade--;
            updateFontMesh();
        } else if (fade == fadeDelay) {
            fade--;
            dir = false;
        } else {
            fontMesh.transform.position = new Vector3(0f, -1000f, 0);
        }
    }

    // Update the font mesh
    private void updateFontMesh() {
        color.a = ((float)fade) / ((float)fadeDelay);
        fontMesh.GetComponent<MeshRenderer>().material.color = color;
        fontMesh.transform.LookAt(head.transform);
        this.transform.position = head.transform.position + head.transform.forward * 2f;
    }

    // Show a string of text
    public void show(string text) {
        TextMesh m = fontMesh.GetComponent<TextMesh>();
        m.text = text;
        dir = true;
    }

    // Reshow a string of text
    public void reshow() {
        dir = true;
    }

    public void setText(string text) {
        TextMesh m = fontMesh.GetComponent<TextMesh>();
        m.text = text;
    }
}
