using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightButtonController : MonoBehaviour
{
    // Events initialising
    public event Action EnableGauge;
    public event Action DisableGauge;
    public event Action<bool> RightButtonPressed;

    //public InteractionController PController;
    //Vector2 JoystickInput;
    public ShapeSpawner SS;

    private void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
        { 
            //PController.TriggerDown = true;
            RightButtonPressed(true);
            EnableGauge(); 
        }
        
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger))  
        { 
            SS.ShootFruit(SS.SingleFruitPrefab); 
            //PController.TriggerDown = false;
            RightButtonPressed(false);
            DisableGauge(); 
        }    
    }
}

