using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover_Show : MonoBehaviour
{
    GameObject fontMesh;
    Collider collide;
    GameObject bar;

    // Start is called before the first frame update
    void Start() {
        fontMesh = GameObject.Find("Label_Display");
        collide = fontMesh.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update() {
        if (bar != null) {
            fontMesh.transform.position = this.transform.position + Vector3.up * 0.05f;
            Vector3 msh = fontMesh.transform.eulerAngles;
            Vector3 ctr = this.transform.rotation.eulerAngles;
            msh.y = ctr.y;
            fontMesh.transform.rotation = Quaternion.Euler(msh);
        } else {
            fontMesh.transform.position = new Vector3(0f, -1000f, 0f);
        }
    }


    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Bar(Clone)" && bar == null) {
            TextMesh m = fontMesh.GetComponent<TextMesh>();
            Bar b = col.gameObject.GetComponent<Bar>();
            print(b);
            m.text = b.getLabel();
            bar = col.gameObject;
        }
    }

    void OnCollisionExit(Collision col) {
        if (bar != null) {
            bar = null;
        }
    }
}
