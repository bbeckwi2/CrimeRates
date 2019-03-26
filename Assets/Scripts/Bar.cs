using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    // The bars drawn on the map
    [Range(0,1000)]
    public float size = 10;

    [Range (0,10)]
    public float stepSize = 0.01f;

    //Offset from the center of the state
    public Vector3 offSet = new Vector3(0f, 0f, 0f);

    private int asc = 1;
    private float cVal = 0.0f;
    private float goal;
    public bool isVisable = true;

    public GameObject barPrefab;
    public GameObject bar;
    private Transform barTransform;
    private Vector3 cPos;

    // Start is called before the first frame update
    void Start() {
        bar = Instantiate(barPrefab);
        cPos = gameObject.transform.position + offSet;
        barTransform = bar.transform;
        size = 8f * Random.value + 2;
        stepSize = Random.Range(0.001f, 0.05f);
        barTransform.position = new Vector3(cPos.x, cPos.y, cPos.z);
        if (!isVisable)
            cVal = 0.0f;
    }

    // Update is called once per frame
    void Update() {

        if (!isVisable)
            return;

        //Demo code
        cVal += stepSize * asc;
        cVal = Mathf.Min(size, Mathf.Max(0, cVal));

        if (cVal == 0.0 || cVal == size) {
            asc *= -1;
        }

        barTransform.position = new Vector3(cPos.x, cPos.y + cVal / 2, cPos.z);
        barTransform.localScale = new Vector3(barTransform.localScale.x, cVal, barTransform.localScale.z);
    }

    //Used to set the height of the bar chart out of 100
    public void SetGoal(float val) {
        goal = val;
    }
}
