using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtonController : MonoBehaviour
{
    public InteractionController PController;
    Vector2 JoystickInput;
    private void Update()
    {
        JoystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        if (PController.Grabbing == true) { PController.ShapeZPosOffset = JoystickInput.y; }
        if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)) { PController.Grabbing = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)) { PController.Release(); PController.Grabbing = false; }
    }
}
