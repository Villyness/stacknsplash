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

    public Vector3[] pos;
    GameObject Currentobject;
    Rigidbody ObjectRB;

    public float MaxReleaseForce;
    public float ForceIncreaseSpeed;
    public float releaseforce;
    public bool TriggerDown;

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
       AudioS = GetComponent<AudioSource>();  //the sound the gun will make when firing 
    }


    private void FixedUpdate()
    {

        if (TriggerDown) { if (releaseforce < MaxReleaseForce) releaseforce = releaseforce + ForceIncreaseSpeed; }
        
        pos = new Vector3[] { transform.position, transform.position + transform.forward }; //setting the positions of the "beams" will probably turn this off later
        holdingShapeObject = ShapespawnerScript.AmmoSingleFruit; //this is just for debugging
    }
 

    public void ChildObject(GameObject a) //turning off most phyisics for object in hand 
    {
        a.GetComponent<MeshCollider>().enabled = false;
        Rigidbody rb = a.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.transform.parent = this.transform;
        rb.useGravity = false;
        rb.isKinematic = true;
        a = holdingShapeObject;
    }


    public void Release(GameObject a) //shooting a gameobject away (gets called in the "shape spawner" script)
    {                                 
        Rigidbody rb = a.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        AudioS.Play();
        rb.AddForce(transform.TransformDirection(Vector3.forward * releaseforce));
        releaseforce = 0;
        print("release");
     
    }



}
