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
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){ PController.TriggerDown = true; }
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))  { SS.ShootFruit(SS.SingleFruitPrefab); PController.TriggerDown = false; }    
    }
}

