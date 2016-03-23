using UnityEngine;
using System.Collections;

public class ControllerAttachmentTracker : MonoBehaviour {

    public bool AttachTo(int deviceIndex)
    {
        attachedDeviceIndex = deviceIndex;
        return isAttached;
    }

    public void Unattach()
    {
        attachedDeviceIndex = -1;
    }

    public bool isAttached
    {
        get
        {
            return attachedDevice != null;
        }
    }

    public SteamVR_Controller.Device attachedDevice
    {
        get
        {
            return SteamVR_Controller.Input(attachedDeviceIndex);
        }
    }

    int attachedDeviceIndex = -1;

	// Use this for initialization
	void Awake ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
