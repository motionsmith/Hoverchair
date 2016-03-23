using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ControllerAttachmentTracker))]
public class ControllerAttachmentVisualizer : MonoBehaviour {

    ControllerAttachmentTracker attachment;
    bool knownAttachState;

	void Awake ()
    {
        attachment = GetComponent<ControllerAttachmentTracker>();
        knownAttachState = attachment.isAttached;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (attachment.isAttached != knownAttachState)
        {
            knownAttachState = attachment.isAttached;
            UpdateState();
        }
	}

    private void UpdateState()
    {
        if (attachment.isAttached)
        {
            Debug.Log("Render as attached.");
        }
        else
        {
            Debug.Log("Render as not attached.");
        }
    }
}
