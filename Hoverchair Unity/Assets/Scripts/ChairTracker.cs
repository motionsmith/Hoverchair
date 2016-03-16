using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ChairTracker : MonoBehaviour {

    public enum ChairTrackMode
    {
        NotAChair,
        AChair
    }

    public ChairTrackMode chairTrackMode
    {
        get; private set;
    }

    public GameObject chairGameObject;

    SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Awake()
    {
        chairTrackMode = ChairTrackMode.NotAChair;

        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            chairTrackMode = (chairTrackMode == ChairTrackMode.AChair) ? ChairTrackMode.NotAChair : ChairTrackMode.AChair;
            chairGameObject.SetActive(chairTrackMode == ChairTrackMode.AChair);
        }
	}
}
