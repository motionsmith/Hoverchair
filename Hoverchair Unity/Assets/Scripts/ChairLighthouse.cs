using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ChairLighthouse : MonoBehaviour {

    public Chair chair;

    SteamVR_TrackedObject trackedObj;
    
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        //Find a chair.
        if (chair == null)
        {
            chair = GameObject.FindObjectOfType<Chair>();
        }
    }
	
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if (chair.lighthouse == transform)
        {
            if (!device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                UnpairChair();
            }
        }
        else if (chair.lighthouse == null)
        {
            if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
            {
                PairChair();
            }
        }
	}

    void PairChair()
    {
        chair.lighthouse = transform;
    }

    void UnpairChair()
    {
        chair.lighthouse = null;
    }
}
