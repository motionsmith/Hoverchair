using UnityEngine;
using System.Collections;

public class LockToTransform : MonoBehaviour {
    public Transform controller;
    public Vector3 positionOffset;

    Transform xform;

	// Use this for initialization
	void Start () {
        xform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        xform.position = controller.position + positionOffset;
	}
}
