using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class ChairLighthouse : MonoBehaviour {

    public Chair chair;

    SteamVR_TrackedObject trackedObj;

    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

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
