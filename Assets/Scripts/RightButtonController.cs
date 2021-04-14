using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightButtonController : MonoBehaviour
{
    public event Action EnableGauge;
    public event Action DisableGauge;
    public InteractionController PController;
    Vector2 JoystickInput;
    public ShapeSpawner SS;
    private void Update()
    {
        //JoystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //if (PController.Grabbing == true) { PController.ShapeZPosOffset = JoystickInput.y; }
        if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
           // EnableGauge();

            if (PController.PointingAtInteractable)
                PController.Grabbing = true;
        }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))
        {
            if (!PController.PointingAtBarrel)
            {
                if (PController.Ammo >= 0)
                {
                    { SS.ShootBannana(SS.SingleBannanaPrefab); PController.Grabbing = false; }
                }
            }
            else { PController.Reload(); }
        }
    }
}

