using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {
    private float size = 10;
    private float stepSize = 0.01f;
    private Vector3 initialScale;

    private float cVal = 0.0f;
    private float goal;
    private float slowDist;
    public bool isVisable = true;

    public GameObject bar;
    private Transform barTransform;
    private Vector3 cPos;
    private float xyScale = 1.0f;
    private float asc = 1.0f;

    public void initialize(GameObject prefab, Vector3 position, float size = 5f) {
        this.bar = Instantiate(prefab);
        this.cPos = position;
        this.barTransform = this.bar.transform;
        this.size = size;
        this.initialScale = barTransform.localScale;
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

        if (cVal == goal) {
            return;
        }

        if (goal - slowDist < cVal && cVal < slowDist + goal) {
            cVal += (stepSize * asc) * Math.Max(Math.Abs(cVal - goal) / slowDist, 0.003f);
        } else {
            cVal += (stepSize * asc);
        }

        if (goal - slowDist * 0.005f < cVal && cVal < goal + slowDist * 0.005f) {
            cVal = goal;
        }

        barTransform.position = new Vector3(cPos.x, cPos.y + cVal / 2, cPos.z);
        barTransform.localScale = new Vector3(barTransform.localScale.x, cVal, barTransform.localScale.z);
    }

    // Set the width and length of the bar
    public void setXYScale(float scale) {
        this.xyScale = scale;
        barTransform.localScale = new Vector3(initialScale.x * scale, barTransform.localScale.y, initialScale.z * scale);
    }

    // Used to set the height of the bar chart out of 100
    public void SetGoal(float val) {
        goal = val;
        slowDist = Math.Min(Math.Abs(cVal - goal) / 4f, 2f);
        if (goal > cVal) {
            asc = 1.0f;
        } else {
            asc = -1.0f;
        }
    }

    // If we want to change the material for whatever reason
    public void setMaterial(Material mat) {
        Material m = this.bar.GetComponent<Material>();
        m = mat;
    }

    public void setColor(Color color) {
        this.bar.GetComponent<Renderer>().material.color = color;
    }

    //Good enough for government work!
    public float getRoughScale() {
        return barTransform.localScale.x;
    }
}
