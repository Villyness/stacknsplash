using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButtonController : MonoBehaviour
{
    public InteractionController PController;
    Vector2 JoystickInput;
    private void Update()
    {
        //JoystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //if (PController.Grabbing == true) { PController.ShapeZPosOffset = JoystickInput.y; }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) { PController.Grabbing = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) { PController.Release(); PController.Grabbing = false; }
    }
}

