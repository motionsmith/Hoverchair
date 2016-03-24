using UnityEngine;
using System.Collections;

public class ChairDriver : MonoBehaviour {
    
    public float maxSpeed; //Meters per second
    public Transform fwdXform;
    public Chair chair;

    CharacterController characterController;
    Transform vrCameraXform;

	// Use this for initialization
	void Awake () {
        
        //Create the character controller.
        if (characterController == null)
        {
            characterController = gameObject.AddComponent<CharacterController>();
            characterController.radius = 0.3f;
            characterController.height = 1f;
        }

        vrCameraXform = gameObject.GetComponentInChildren<Camera>().GetComponent<Transform>();

        //Find a chair.
        if (chair == null)
        {
            chair = GameObject.FindObjectOfType<Chair>();
        }

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
                Vector3 velocity = fwdXform.forward * maxSpeed * chair.acceleratorStrength;
                velocity.y = 0;
                characterController.Move(velocity * Time.deltaTime);
            } else
            {
                var colliderPosition = vrCameraXform.localPosition;
                colliderPosition.y = characterController.radius + 0.2f;
                characterController.center = colliderPosition;
            }
        }
	}
}
