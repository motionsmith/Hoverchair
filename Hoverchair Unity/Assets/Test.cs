using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = transform.up * -1f;
        dir.y = 0;
        Debug.DrawRay(transform.position, dir, Color.red);
	}
}
