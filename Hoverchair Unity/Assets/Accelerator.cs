using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour {

    public AnimationCurve angleToStrength;

    float _strength = 0;
    public float strength
    {
        get
        {
            return _strength;
        }

        set
        {
            _strength = Mathf.Clamp(value, 0, 1);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
