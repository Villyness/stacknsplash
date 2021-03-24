using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTriggerRelease : MonoBehaviour
{
    public InteractionController PController;
    private void Update()
    {

        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)) { PController.Grabbing = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)) { PController.Release(); PController.Grabbing = false; }
    }
}
