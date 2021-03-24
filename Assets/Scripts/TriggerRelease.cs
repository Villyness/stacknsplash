using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRelease : MonoBehaviour
{
    public InteractionController PController;
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) { PController.Grabbing = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) { PController.Release(); PController.Grabbing = false; }
    }
}

