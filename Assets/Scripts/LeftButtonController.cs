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
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) { PController.TriggerDown = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger)){ SS.ShootFruit(SS.SingleFruitPrefab); PController.TriggerDown = false; }
    }
}
