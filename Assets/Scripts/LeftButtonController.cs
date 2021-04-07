using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtonController : MonoBehaviour
{
    public InteractionController PController;
    Vector2 JoystickInput;
    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))  PController.Grabbing = true;
        // else PController.Grabbing = false;
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)) { PController.Grabbing = false; }
    }
}
