using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChargeGauge : MonoBehaviour
{
    private Canvas ownCanvas;
    private Slider loadGauge;

    //some inefficient stuff to put in functionality for now
    //private float gaugeMax;
    // Start is called before the first frame update
    void Start()
    {
        ownCanvas = GetComponent<Canvas>();
        //GaugeStart();
        //Findnt<RightButtonController>().EnableGauge += GaugeStart;
        FindObjectOfType<RightButtonController>().EnableGauge += GaugeStart;
        loadGauge.maxValue = FindObjectOfType<InteractionController>().MaxReleaseForce;
    }

    // Update is called once per frame
    void FixedUpdate()  //needs optimising later
    {
        while (ownCanvas.enabled == true)
        {
            // there are more than one InteractionControllers. Need to refract the code there.
            Debug.Log(FindObjectOfType<InteractionController>().releaseforce);
            loadGauge.value = FindObjectOfType<InteractionController>().releaseforce; 
        }
    }

    public void GaugeStart()
    {
        ownCanvas.enabled = true;
    }
}
