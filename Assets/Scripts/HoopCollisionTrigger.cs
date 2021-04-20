using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCollisionTrigger : MonoBehaviour
{
    public HoopDisapear HD;
    // Start is called before the first frame update
    void Start()
    {
        HD = GetComponentInParent<HoopDisapear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("hit");
        HD.disapear = true;       
    }
}
