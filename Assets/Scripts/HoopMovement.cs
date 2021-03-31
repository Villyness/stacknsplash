using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    public GameObject PlayerPlatform;
    public float HoopVelocity;
    public bool DirectionChange = false;
    public float Range;
    public float changetimer;
    public bool changedFirstTime = false;
    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
        transform.RotateAround(PlayerPlatform.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);

        if (DirectionChange == false) { changetimer += 1 *Time.deltaTime; }
        
        if (changetimer > Range || changedFirstTime == false && changetimer*2 > Range)
        {
            changedFirstTime = true;
            DirectionChange = true;
            ChangeDirection();
            changetimer = 0;
        }
        else
        {
            DirectionChange = false;
        }
        
   
    }

    void ChangeDirection()
    {
        HoopVelocity *= -1;
    }
}
