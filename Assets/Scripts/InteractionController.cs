using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    /// <summary>
    /// the distance the player can grab objects
    /// </summary>
    public float Reach;
    public ShapeSpawner ShapespawnerScript;
    AudioSource AudioS;
    // Update is called once per frame

    bool pickedUpFirstTime = false;
    public LineRenderer Beam;
    public Vector3[] pos;
    GameObject Currentobject;
    Rigidbody ObjectRB;

    public float MaxReleaseForce;
    public float ForceIncreaseSpeed;
    public float releaseforce;
    public float MoveSpeed;
    public float ShapeMinDistance;
    public bool Grabbing;
    public bool PointingAtInteractable = false;
    public bool HoldingShape;

    public int Ammo;
    public bool Aiming;
    bool release;
    public bool PointingAtBarrel;
    RaycastHit hit;

    public GameObject InFrontOfController;

    //for debugging
    public GameObject holdingShapeObject;


    // Start is called before the first frame update
    void Start()
    {
       Ammo = 7;
       AudioS = GetComponent<AudioSource>();  //the sound the gun will make when firing 
    }


    private void FixedUpdate()
    {
        //This is supposed to be a check to see whether the controller is pointing at the reload barrel. (Checjs for hit and also checks if that hit is barrel.
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) //this is supposed to be a check whether 
        {
            InFrontOfController = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            if (hit.collider.tag == ("Bannana Bucket")) { PointingAtBarrel = true; print("truuue"); }
            else { PointingAtBarrel = false; }
        }
        else { PointingAtBarrel = false; }
        if (releaseforce < MaxReleaseForce)
        {
            releaseforce = releaseforce + ForceIncreaseSpeed;
        }

        pos = new Vector3[] { transform.position, transform.position + transform.forward }; //setting the positions of the "beams" will probably turn this off later
        holdingShapeObject = ShapespawnerScript.AmmoSingleBannana; //this is just for debugging

    }
    private void Update()
    {
        DrawLines();
    }

    void DrawLines()
    {
        Beam.SetPositions(pos);
    }

    public void ChildObject(GameObject a) //turning off most phyisics for the shape when its childed
    {
        Rigidbody rb = a.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.transform.parent = this.transform;
        rb.useGravity = false;
        rb.isKinematic = true;
        a = holdingShapeObject;
    }

    public void Release(GameObject a) //releasing the shape (gets called in the R/L button scripts)
    {
        if (PointingAtBarrel) { Ammo = 7; AudioS.Play(); }  //this is meant to be a reload 
        else //If not pointing at Ammo Barrel, "Fire"
        {
            Ammo--;                                     
            Rigidbody rb = a.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            AudioS.Play();
            rb.AddForce(transform.TransformDirection(Vector3.forward * releaseforce));
            releaseforce = 0;
            print("release");
        }
    }



}
