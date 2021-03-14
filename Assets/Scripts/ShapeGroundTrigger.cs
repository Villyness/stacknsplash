using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGroundTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public SplodyTriggers ExplosionTrigger;

    private void OnCollisionEnter(Collision collision)
    {
        print("collision");
        StartCoroutine(ExplosionTrigger.PartyExplosion());
    }
}
