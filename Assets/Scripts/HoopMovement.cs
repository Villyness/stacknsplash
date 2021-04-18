using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    public GameObject PlayerPlatform;
    public float HoopVelocity;
    public float Range;
    public float changetimer;
    public bool changedFirstTime = false;
    // Update is called once per frame
    private void Start()
    {
        PlayerPlatform = GameObject.FindGameObjectWithTag("Player");
        if (Random.value < 0.5f) { HoopVelocity = Random.Range(-20, -10); }
        else { HoopVelocity = Random.Range(10, 20); }
    }
    void Update()
    {
        transform.RotateAround(PlayerPlatform.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);   
    }


}
