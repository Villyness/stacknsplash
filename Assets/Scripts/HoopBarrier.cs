using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopBarrier : MonoBehaviour
{
    public bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        triggered = true;
        other.gameObject.GetComponent<HoopMovement>().HoopVelocity = other.gameObject.GetComponent<HoopMovement>().HoopVelocity *- 1;
    }
}
