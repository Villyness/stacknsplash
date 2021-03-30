using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    public GameObject PlayerPlatform;
    public float HoopVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(PlayerPlatform.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);
    }
}
