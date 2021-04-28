using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeSpawner : MonoBehaviour
{
    //this script spawns the fruit guns as the controllers and instantiates the new fruit objects that are getting thrown
    public InteractionController IC;

    public GameObject SingleFruitPrefab;
    public GameObject FruitInHand;

    public GameObject AmmoSingleFruit;
    public Vector3 ShapePos;
    public bool Apple;

    void Start()
    {
        SpawnFruitinHand(SingleFruitPrefab);
    }
    // Update is called once per frame
    void Update()
    {
        if (FruitInHand == null) { SpawnFruitinHand(SingleFruitPrefab); } //checking that the fruit gun isnt null
    }

    public void ShootFruit(GameObject g)  //shoot a piece of fruit from your hand. 
    {
        AmmoSingleFruit = Instantiate(g, transform.position + (transform.forward/4), Random.rotation);
        IC.Release(AmmoSingleFruit); //this doing physics stuff to the object in the Interaction controlelr script
    }
    void SpawnFruitinHand(GameObject g) //spawn the gun
    { 
       //the apple and the bannana needed different rotations for the gun
      if (Apple) FruitInHand = Instantiate(g, transform.position + (transform.forward/8), transform.rotation * Quaternion.Euler(90, 180,0)); 
      else FruitInHand = Instantiate(g, transform.position + (transform.forward/6), transform.rotation * Quaternion.Euler(145, 180,0));
        IC.ChildObject(FruitInHand); //this is childing the object to the hand and turning offf physics
    }

}
