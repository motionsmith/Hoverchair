using UnityEngine;
using System.Collections;

public class ChairDriver : MonoBehaviour {
    
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

        //Get the Camera
        vrCameraXform = gameObject.GetComponentInChildren<Camera>().GetComponent<Transform>();

        //Find a chair.
        if (chair == null)
        {
            chair = GameObject.FindObjectOfType<Chair>();
        }
	}
	
	void FixedUpdate () {
        if (chair.lighthouse != null)
        {
            //If the player is leaning back, then we're going to move the play area
            //via the CharacterController attached to it.
            if (chair.acceleratorStrength > 0)
            {
                Vector3 velocity = chair.forwardXform.forward * chair.acceleratorStrength;
                velocity.y = 0;
                characterController.Move(velocity * Time.deltaTime);
                //Throw some gravity in right here.

            } else
            //If the player is not leaning back, then we're going to reposition the character controller
            //to center on the player.
            //We can't "move" the play area and reposition the character controller because doing so would
            //invalidate the move's collision detection.
            {
                var colliderPosition = vrCameraXform.localPosition;
                colliderPosition.y = characterController.radius + 0.2f;
                characterController.center = colliderPosition;
            }
        }
	}
}
