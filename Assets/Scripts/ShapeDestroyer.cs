using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDestroyer : MonoBehaviour
{
    public GameObject Player;
    public int DestroyDistance;
    float shapedistance;
    bool TooFar;

    private void Start()
    {
        Player = GameObject.Find("PlayerController");
    }

    private void FixedUpdate()
    {
        shapedistance = Vector3.Distance(Player.transform.position, this.transform.position);
        if (shapedistance > DestroyDistance) 
        {
            Destroy(this.gameObject);
                
        };
    }
    private void OnCollisionStay(Collision collision)
    {
        Destroy(this.gameObject);
    }

}
