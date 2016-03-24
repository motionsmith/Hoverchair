using UnityEngine;
using System.Collections;

public class ChairDriver : MonoBehaviour {

    public AnimationCurve angleToStrength;
    public float maxSpeed; //Meters per second
    public Transform fwdXform;
    public Chair chair;

    CharacterController characteController;
    Transform vrCameraXform;

	// Use this for initialization
	void Awake () {
        characteController = gameObject.AddComponent<CharacterController>();
        characteController.radius = 0.3f;
        characteController.height = 1f;

        vrCameraXform = gameObject.GetComponentInChildren<Camera>().GetComponent<Transform>();

        //Find a chair.
        if (chair == null)
        {
            chair = GameObject.FindObjectOfType<Chair>();
        }
        //characteController = chair.GetComponentInChildren<CharacterController>();

        //Find a forward transform. Use the chair's if one is not available.
        if (fwdXform == null)
        {
            fwdXform = chair.transform;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (chair.lighthouse != null)
        {
            if (chair.acceleratorStrength > 0)
            {
                Vector3 velocity = fwdXform.forward * maxSpeed * angleToStrength.Evaluate(chair.acceleratorStrength);
                velocity.y = 0;
                characteController.Move(velocity * Time.deltaTime);
            } else
            {
                var colliderPosition = vrCameraXform.localPosition;
                colliderPosition.y = characteController.radius + 0.2f;
                characteController.center = colliderPosition;

            }
        }
	}
}
