using UnityEngine;
using System.Collections;

public class MimicAngle : MonoBehaviour {

    [System.Serializable]
    public class MimicSettings
    {
        public float fromAngle;
        public float toAngle;

        public float smallerAngle
        {
            get
            {
                return Mathf.Min(fromAngle, toAngle);
            }
        }

        public float largerAngle
        {
            get
            {
                return Mathf.Max(fromAngle, toAngle);
            }
        }
    }

	public Transform lighthouseXform;
    public MimicSettings lighthouseConfig;
    public MimicSettings chairConfig;

	Transform chairXform;

	void Awake () {
		chairXform = GetComponent<Transform> ();
	}

	void Update () {
        if (lighthouseXform == null) {
            return;
        }

		Vector3 newRotation = chairXform.localEulerAngles;

        //The lighthouse's tilt angle on the X-Axis.
        float lighhouseLeanAngle = Mathf.Clamp(lighthouseXform.localEulerAngles.x, lighthouseConfig.smallerAngle, lighthouseConfig.largerAngle);

        //This restricts the controller-reading to only when the controller is "face down" (track pad looking at the floor).
        //If the track pad is looking at the ceiling, then we just put the chair upright.
        if (lighthouseXform.localEulerAngles.z < 90f || lighthouseXform.localEulerAngles.z > 270f)
        {
            lighhouseLeanAngle = lighthouseConfig.fromAngle;
        }
        
        //Some maths to make the chair's back match the leaning angle of the lighthouse sensor.
        float lighthouseLeanPct = (lighhouseLeanAngle - lighthouseConfig.fromAngle) / (lighthouseConfig.toAngle - lighthouseConfig.fromAngle); //Turns the angle the lighthouse is leaning into a fraction from 0 (upright) to 1 (face down).
        float chairLeanOffset = (chairConfig.toAngle - chairConfig.fromAngle) * lighthouseLeanPct;
        newRotation.x = chairConfig.fromAngle + chairLeanOffset;

        chairXform.localEulerAngles = newRotation;
	}
}
