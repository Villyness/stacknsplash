using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeftButtonController : MonoBehaviour
{
    // V's comment tasklist
    //

    //public InteractionController PController;   // Prolly doesn't need to be public, can set it to do an event.
    //Vector2 JoystickInput;
    public ShapeSpawner SS;
    public event Action EnableGauge;
    public event Action DisableGauge;
    public event Action<bool> LeftButtonPressed;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) 
        { 
            //PController.TriggerDown = true;
            LeftButtonPressed(true);
            EnableGauge(); 
        }

        if (OVRInput.GetUp(OVRInput.RawButton.LIndexTrigger))
        { 
            SS.ShootFruit(SS.SingleFruitPrefab);
            LeftButtonPressed(false);
            DisableGauge(); 
        }
    }
}
