using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class ChairDriver : MonoBehaviour {

    public Accelerator accelerator;
    public float maxSpeed; //Meters per second

    CharacterController characteController;

	// Use this for initialization
	void Awake () {
        characteController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 velocity = Vector3.forward;
        velocity.z = accelerator.strength * maxSpeed;
        characteController.Move(velocity * Time.deltaTime);
	}
}
