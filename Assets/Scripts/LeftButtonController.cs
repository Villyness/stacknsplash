using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButtonController : MonoBehaviour
{
    public InteractionController PController;
    Vector2 JoystickInput;
    public ShapeSpawner SS;
    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) PController.Grabbing = true;
        // else PController.Grabbing = false;
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
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
