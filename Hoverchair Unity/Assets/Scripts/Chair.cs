using UnityEngine;
using System.Collections;
using System;

public class Chair : MonoBehaviour {
    
    public float noThrottleLeanAngle = 0;
    public float fullThrottleLeanAngle;
    public Transform throttleXform;
    public float chairBackRestingAngle;

    Transform xform;
    Transform _lighthouse;
    CharacterController characteController;

    public float acceleratorStrength
    {
        get; private set;
    }

    public Transform lighthouse
    {
        get
        {
            return _lighthouse;
        }
        set
        {
            if (_lighthouse == value)
            {
                return;
            }

            _lighthouse = value;
            if (_lighthouse)
            {
                RenderChairActive();
            }
            else
            {
                RenderChairInactive();
            }
        }
    }

    // Use this for initialization
    void Awake () {
        acceleratorStrength = 0;
        xform = GetComponent<Transform>();
        characteController = gameObject.GetComponentInChildren<CharacterController>();

        RenderChairInactive();
    }
	
	// Update is called once per frame
	void Update () {
        //Determine what percent the chair is leaned back (0-1)
        if (lighthouse != null)
        {
            //From 0 to 1, (amount chair is leaned back) / (maximum leanback) 
            acceleratorStrength = (throttleXform.localEulerAngles.x - noThrottleLeanAngle) / (fullThrottleLeanAngle - noThrottleLeanAngle);

            Vector3 newRotation = throttleXform.localEulerAngles;

            //The lighthouse's tilt angle on the X-Axis.
            float lighhouseLeanAngle = Mathf.Clamp(lighthouse.localEulerAngles.x, 270, 359);

            //This restricts the controller-reading to only when the controller is "face down" (track pad looking at the floor).
            //If the track pad is looking at the ceiling, then we just put the chair upright.
            if (lighthouse.localEulerAngles.z < 90f || lighthouse.localEulerAngles.z > 270f)
            {
                lighhouseLeanAngle = 270;
            }

            //Some maths to make the chair's back match the leaning angle of the lighthouse sensor.
            float lighthouseLeanPct = (lighhouseLeanAngle - 270) / 90; //Turns the angle the lighthouse is leaning into a fraction from 0 (upright) to 1 (face down).
            float chairLeanOffset = 90 * lighthouseLeanPct;
            newRotation.x = chairBackRestingAngle - chairLeanOffset;

            throttleXform.localEulerAngles = newRotation;
        } else
        {
            acceleratorStrength = 0;
        }
    }

    void LateUpdate()
    {
        SyncChairRotationToLighthouse();
        SyncChairPositionToLighthouse();
    }

    void RenderChairActive()
    {
        
    }

    void RenderChairInactive()
    {
        
    }

    //update rotation of chair to match the lighthouse.
    void SyncChairRotationToLighthouse()
    {
        if (lighthouse)
        {
            Vector3 newRotation = xform.localEulerAngles;
            newRotation.y = lighthouse.localEulerAngles.y;

            xform.localEulerAngles = newRotation;
        }
    }

    void SyncChairPositionToLighthouse()
    {
        if (lighthouse)
        {
            Vector3 newPosition = xform.position;
            newPosition = lighthouse.position;

            xform.position = newPosition;
        }
    }
}