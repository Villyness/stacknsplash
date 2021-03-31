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
    public float MaxReleaseForce;
    public float ForceIncreaseSpeed;
    float releaseforce;
    public float MoveSpeed;
    public float ShapeMinDistance;
    public bool Grabbing;
    public bool PointingAtInteractable = false;

    //for debugging
    public GameObject holdingShape;


    // Start is called before the first frame update
    void Start()
    {
        Beam = this.gameObject.GetComponent<LineRenderer>();
        Beam.startWidth = 0.01f;
        Beam.endWidth = 0.01f;
    }

    
    private void FixedUpdate()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Reach))    //checks if raycast hits anything
        {
            if (hit.collider.CompareTag("Interactable")) { PointingAtInteractable = true; }
           
            if (PointingAtInteractable == false) { DrawLines(); Grabbing = false; }
        }
        else { DrawLines(); PointingAtInteractable = false; Grabbing = false; }
        
        if (PointingAtInteractable && Grabbing)
        {

            pos = new Vector3[] { transform.position, hit.transform.position };              //start pos from controller and hit pos put in an array
            Beam.SetPositions(pos);
            //setting the positions of the line renderer

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            print("grabbing something");

            Beam.material.color = Color.green;

            if (pickedUpFirstTime != true)
            {
                ChildObject();
                pickedUpFirstTime = true;
            }

            if (hit.distance > ShapeMinDistance)
            {
                //pulling the shape closer depending on the shape offset (that gets changed in the button controler script)
                hit.transform.position = hit.transform.position +
                    (transform.TransformDirection(Vector3.back) * Time.deltaTime * MoveSpeed);
            }
            else
            {
                if (releaseforce < MaxReleaseForce)
                {
                    releaseforce = releaseforce + ForceIncreaseSpeed;
                }

            }
        }

        holdingShape = hit.transform.gameObject;
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

        hit.rigidbody.AddForce(transform.TransformDirection(Vector3.forward * releaseforce));
    }
                   
}
