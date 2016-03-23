using UnityEngine;
using System.Collections;

public class Accelerator : MonoBehaviour {

    public AnimationCurve angleToStrength;
    public float noThrottleBackAngle;
    public float fullThrottleBackAngle;
    public Transform throttleXform;

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
	
	// Update is called once per frame
	void Update () {
        float backTiltRange = fullThrottleBackAngle - noThrottleBackAngle;
        float backTiltOffset = throttleXform.localEulerAngles.x - noThrottleBackAngle;
        strength = backTiltOffset / backTiltRange;
	}
}
