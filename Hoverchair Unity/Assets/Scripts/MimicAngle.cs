using UnityEngine;
using System.Collections;

public class MimicAngle : MonoBehaviour {

	[System.Serializable]
	public enum RotationAxis {
		x,
		y,
		z
	}

	public Transform sourceXform;
	public RotationAxis sourceAxis;
	public RotationAxis destinationAxis;
	public float rotationalOffset = 0;
	public float minimumAngle = 0;
	public float maximumAngle = 360;

	Transform destinationXform;

	void Awake () {
		destinationXform = GetComponent<Transform> ();
	}

	void Update () {
        if (sourceXform == null) {
            return;
        }

		Vector3 newRotation = destinationXform.localEulerAngles;

		float sourceAxisValue = sourceXform.localEulerAngles.x;
		if (sourceAxis == RotationAxis.y) {
			sourceAxisValue = sourceXform.localEulerAngles.y;
		} else if (sourceAxis == RotationAxis.z) {
			sourceAxisValue = sourceXform.localEulerAngles.z;
		}

		if (destinationAxis == RotationAxis.x) {
			newRotation.x = Mathf.Clamp((sourceAxisValue + rotationalOffset) % 360, minimumAngle, maximumAngle);
			Debug.Log (newRotation);
		} else if (destinationAxis == RotationAxis.y) {
			newRotation.y = sourceAxisValue;
		} else {
			newRotation.z = sourceAxisValue;
		}

		destinationXform.localEulerAngles = newRotation;
	}
}
