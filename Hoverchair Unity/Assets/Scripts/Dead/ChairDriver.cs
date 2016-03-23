using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ChairDriver : MonoBehaviour {

    public AnimationCurve angleToStrength;
    public float maxSpeed; //Meters per second
    public Transform fwdXform;
    public Chair chair;

    CharacterController characteController;

	// Use this for initialization
	void Awake () {
        characteController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (chair.lighthouse != null)
        {
            Vector3 velocity = fwdXform.forward * maxSpeed * chair.acceleratorStrength;
            velocity.y = 0;
            characteController.Move(velocity * Time.deltaTime);
        }
	}
}
