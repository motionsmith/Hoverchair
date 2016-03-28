using UnityEngine;
using Valve.VR;
using System.Collections;
using System;

public class Chair : MonoBehaviour {
    
    public float noThrottleLeanAngle = 0;
    public float fullThrottleLeanAngle;
    public Transform throttleXform;
    public float chairBackRestingAngle;
    public AnimationCurve accelerationOfLean = AnimationCurve.Linear(0, 0, 1, 1);
    public float maxSpeed = 1; //Meters per second
    public Transform forwardXform;

    Transform xform;
    Transform _lighthouse;
    float _acceleratorStrength = 0;
    bool foundNewTrackedObject = true;

    public float acceleratorStrength
    {
        get
        {
            return accelerationOfLean.Evaluate(_acceleratorStrength) * maxSpeed;
        }
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
        xform = GetComponent<Transform>();

        RenderChairInactive();
    }

    void Start()
    {
        //Set up the chair driver on the play area to move the play area around (and thus move the player).
        SteamVR_PlayArea playArea = GameObject.FindObjectOfType<SteamVR_PlayArea>();
        if (playArea != null)
        {
            ChairDriver chairDriver = playArea.GetComponent<ChairDriver>();
            if (chairDriver == null)
            {
                chairDriver = playArea.gameObject.AddComponent<ChairDriver>();
            }
        }
        else
        {
            Debug.LogError("The hoverchair cannot setup because it didn't find a SteamVR_PlayArea component in the scene.");
            gameObject.SetActive(false);
        }
    }

    void LateUpdate()
    {
        SyncChairRotationToLighthouse();
        SyncChairPositionToLighthouse();
        SyncChairBackLeanToLighthouse();

        if (foundNewTrackedObject)
        {
            MakeTrackedObjectsChairLighthouses();
            foundNewTrackedObject = false;
        }
    }

    void OnEnable()
    {
        SteamVR_Utils.Event.Listen("device_connected", OnDeviceConnected);
    }

    void OnDisable()
    {
        SteamVR_Utils.Event.Remove("device_connected", OnDeviceConnected);
    }

    //Called when the chair starts being tracked by the lighthouse.
    //Visually signifies that it is properly represented in the scene.
    void RenderChairActive()
    {
        
    }

    //Called when the chair becomes not tracked by the lighthouse
    //to signify that it may not be where it is rendered in the scene.
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

    //Update position of chair to match the lighthouse.
    void SyncChairPositionToLighthouse()
    {
        if (lighthouse)
        {
            Vector3 newPosition = xform.position;
            newPosition = lighthouse.position;

            xform.position = newPosition;
        }
    }

    //Update the chair's back to match the lean of the real chair.
    void SyncChairBackLeanToLighthouse()
    {
        //Determine what percent the chair is leaned back (0-1)
        if (lighthouse != null)
        {
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

            //From 0 to 1, (amount chair is leaned back) / (maximum leanback) 
            _acceleratorStrength = (throttleXform.localEulerAngles.x - noThrottleLeanAngle) / (fullThrottleLeanAngle - noThrottleLeanAngle);
        }
        else
        {
            _acceleratorStrength = 0;
        }
    }

    void OnDeviceConnected(params object[] args)
    {
        int index = (int)args[0];
        SteamVR vr = SteamVR.instance;

        //We're not concerned with this device if it's not a controller.
        if (vr.hmd.GetTrackedDeviceClass((uint)index) != ETrackedDeviceClass.Controller)
            return;

        //Attach a script onto the controller that makes it a chair lighthouse.
        bool connected = (bool)args[1];
        if (connected)
        {
            //Flags the next frame to do a search for new tracked objects.
            foundNewTrackedObject = true;
        }
    }

    //Searches for all tracked objects and attaches the ChairLighthouse script to them so that they can be chair controllers potentially.
    void MakeTrackedObjectsChairLighthouses()
    {
        SteamVR_TrackedObject[] trackedObjects = GameObject.FindObjectsOfType<SteamVR_TrackedObject>();
        for (var i = 0; i < trackedObjects.Length; i++)
        {
            ChairLighthouse chairLighthouse = trackedObjects[i].GetComponent<ChairLighthouse>();
            if (chairLighthouse == null)
            {
                trackedObjects[i].gameObject.AddComponent<ChairLighthouse>();
            }

        }
    }
}