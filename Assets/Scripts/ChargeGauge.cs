using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChargeGauge : MonoBehaviour
{
    private Canvas ownCanvas;
    private Slider loadGauge;
    public GameObject HandAnchor;
    public bool IsRightController;
    //public GameObject Controller;
    private InteractionController refValue;

    //some inefficient stuff to put in functionality for now
    //private float gaugeMax;
    // Start is called before the first frame update
    void Start()
    {
        ownCanvas = GetComponent<Canvas>();
        loadGauge = GetComponentInChildren<Slider>();
        //GaugeStart();
        //Findnt<RightButtonController>().EnableGauge += GaugeStart;
        if (IsRightController == true)
        {
            FindObjectOfType<RightButtonController>().EnableGauge += GaugeStart;
            FindObjectOfType<RightButtonController>().DisableGauge += GaugeStop;
        }
        else
        {
            FindObjectOfType<LeftButtonController>().EnableGauge += GaugeStart;
            FindObjectOfType<LeftButtonController>().DisableGauge += GaugeStop;
        }
        refValue = HandAnchor.GetComponent<InteractionController>();
        loadGauge.maxValue = refValue.MaxReleaseForce;
    }

    // Update is called once per frame
    void FixedUpdate()  //needs optimising later
    {
        if (ownCanvas.enabled == true)
        {
            loadGauge.value = refValue.releaseforce;
            Debug.Log("Hi");
        }
    }

    public void GaugeStart()
    {
        ownCanvas.enabled = true;
    }

    public void GaugeStop()
    {
        ownCanvas.enabled = false;
    }
}
