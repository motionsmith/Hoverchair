using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Accelerator))]
public class ChairDriver : MonoBehaviour {

    CharacterController characteController;
    Accelerator accelerator;

	// Use this for initialization
	void Awake () {
        characteController = gameObject.GetComponent<CharacterController>();
        accelerator = gameObject.GetComponent<Accelerator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
