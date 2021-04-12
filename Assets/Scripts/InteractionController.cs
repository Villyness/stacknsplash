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
      //  Beam = this.gameObject.GetComponent<LineRenderer>();
      //  Beam.startWidth = 0.01f;
      //  Beam.endWidth = 0.01f;
       // DrawLines();
        AudioS = GetComponent<AudioSource>();
    }


    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            InFrontOfController = hit.collider.gameObject;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);
            if (hit.collider.tag == ("Bannana Bucket")) { PointingAtBarrel = true; print("truuue"); }
            else { PointingAtBarrel = false; }
        }
        else {  }
        if (releaseforce < MaxReleaseForce)
        {
            releaseforce = releaseforce + ForceIncreaseSpeed;
        }
        pos = new Vector3[] { transform.position, transform.position + transform.forward };
        holdingShapeObject = ShapespawnerScript.AmmoSingleBannana;

    }
    private void Update()
    {
       
       // 
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

    public void Release(GameObject a) //releasing the shape (gets called in the trigger release scripts)
    {
        if (PointingAtBarrel) { Ammo = 7; AudioS.Play(); }
        Ammo--;
        Rigidbody rb = a.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        AudioS.Play();
       // this.transform.DetachChildren();
        // pickedUpFirstTime = false;
        rb.AddForce(transform.TransformDirection(Vector3.forward * releaseforce));
        releaseforce = 0;
       // ShapespawnerScript.NewShape();
        print("release");
    }



}
