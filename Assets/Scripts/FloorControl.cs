using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class FloorControl : MonoBehaviour
{

    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Boolean upAction;
    public SteamVR_Action_Boolean downAction;

    public Transform camera;
    public Transform floor;

    [Range(1, 100)]
    public float maxHeight = 10.0f;
    private float minHeight = 0.0f;

    [Range(1,100)]
    public float stepSize = 1.0f;

    [Range(1, 1000)]
    public int skipDelay = 100;
    private int skip = 0;

    // Start is called before the first frame update
    void Start() {
        minHeight = floor.position.y;
    }

    // Update is called once per frame
    void Update(){
        if (upAction.GetState(handType)) {
            if (skip == 0) {
                up();
                skip++;
            }
            skip = (skip + 1) % skipDelay;

        } else if (downAction.GetState(handType)) {
            if (skip == 0) {
                down();
                skip++;
            }
            skip = (skip + 1) % skipDelay;
        } else {
            skip = 0;
        }
    }

    private void up() {
        if (floor.position.y < maxHeight) {
            float y = Mathf.Min(floor.position.y + stepSize, maxHeight);
            float diff = y - floor.position.y;
            camera.position = new Vector3(camera.position.x, camera.position.y + diff, camera.position.z);
            floor.position = new Vector3(floor.position.x, y, floor.position.z);
        }
    }

    private void down() {
        if (floor.position.y > minHeight) {
            float y = Mathf.Max(floor.position.y - stepSize, minHeight);
            float diff = y - floor.position.y;
            camera.position = new Vector3(camera.position.x, camera.position.y + diff, camera.position.z);
            floor.position = new Vector3(floor.position.x, y, floor.position.z);
        }
    }
}
