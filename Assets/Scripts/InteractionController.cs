using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    /// <summary>
    /// the distance the player can grab objects
    /// </summary>
    public float Reach;
    // Update is called once per frame

    bool pickedUpFirstTime = false;
    LineRenderer Beam;
    Vector3[] pos;
    RaycastHit hit;
    float releasetimer;
    public bool Grabbing;
    // Start is called before the first frame update
    void Start()
    {
        Beam = this.gameObject.GetComponent<LineRenderer>();
        Beam.startWidth = 0.1f;
        Beam.endWidth = 0.1f;
    }

    
    private void FixedUpdate()
    {

        if (Grabbing != true) { DrawLines(); }

        
        if (Grabbing == true && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Reach))
        {
            pos = new Vector3[] { transform.position, hit.transform.position };              //start pos from controller and hit pos put in an array
            Beam.SetPositions(pos);                                                                         //setting the positions of the line renderer
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //  Debug.DrawLine(transform.position, hit.transform.position);
            Debug.Log("Did Hit");
            Beam.material.color = Color.green;

            if (hit.collider.tag == "Interactable")
            {
                if (pickedUpFirstTime != true)
                {
                    ChildObject();
                    pickedUpFirstTime = true;
                }
            }
        }

    }

    void DrawLines()
    {
        pos = new Vector3[] { transform.position, transform.TransformDirection(Vector3.forward) * Reach };
        Beam.SetPositions(pos);
    }

    void ChildObject() //turning off most phyisics for the shape when its childed
    {
        hit.rigidbody.velocity = Vector3.zero;
        hit.transform.parent = this.transform;
        hit.rigidbody.useGravity = false;
        hit.rigidbody.isKinematic = true;
    }

    public void Release() //releasing the shape (gets called in the trigger release scripts)
    {
        hit.rigidbody.useGravity = true;
        hit.rigidbody.isKinematic = false;
        transform.DetachChildren();
        pickedUpFirstTime = false;
        releasetimer = 0;
    }
}
