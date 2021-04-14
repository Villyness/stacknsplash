using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeSpawner : MonoBehaviour
{

    //This script handles the logicc for turning off banannas in the bananna gun (to indicate ammo) 
    //and instantiates a single bannanna in front of the gun when fired (the physics logic is in the Interaction Controlller script)
    public InteractionController IC;
    public GameObject BannanaGunPrefab;
    public GameObject SingleBannanaPrefab;
    GameObject BannanaGun;
    public GameObject AmmoSingleBannana;
    public int ShapeDistanceFrom;
    public float CurrentShapeNo = 0;
    public int NextShapeNumber;
    public Vector3 ShapePos;
    public GameObject[] Bannanas;


    void Start()
    {
        SpawnBannanaGun(BannanaGunPrefab);

        Bannanas[0] = BannanaGun.transform.GetChild(0).gameObject;  //these are the different banannas in the bannana gun  
        Bannanas[1] = BannanaGun.transform.GetChild(1).gameObject; 
        Bannanas[2] = BannanaGun.transform.GetChild(2).gameObject; 
        Bannanas[3] = BannanaGun.transform.GetChild(3).gameObject; 
        Bannanas[4] = BannanaGun.transform.GetChild(4).gameObject; 
        Bannanas[5] = BannanaGun.transform.GetChild(5).gameObject; 
        Bannanas[6] = BannanaGun.transform.GetChild(6).gameObject; 
            
    }

    // Update is called once per frame
    void Update()
    {
        switch (IC.Ammo)    //this just handles the "different stages" of ammo and what the bannana gun will look like. we just turn the objects on or off
        {
            case 0:
                Bannanas[0].SetActive(false);
                Bannanas[1].SetActive(false);
                Bannanas[2].SetActive(false);
                Bannanas[3].SetActive(false);
                Bannanas[4].SetActive(false);
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 1:
                Bannanas[1].SetActive(false);
                Bannanas[2].SetActive(false);
                Bannanas[3].SetActive(false);
                Bannanas[4].SetActive(false);
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 2:
                Bannanas[2].SetActive(false);
                Bannanas[3].SetActive(false);
                Bannanas[4].SetActive(false);
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 3:
                Bannanas[3].SetActive(false);
                Bannanas[4].SetActive(false);
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 4:
                Bannanas[4].SetActive(false);
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 5:
                Bannanas[5].SetActive(false);
                Bannanas[6].SetActive(false);
                break;
            case 6:
                Bannanas[6].SetActive(false);
                break;
            case 7:
                Bannanas[0].SetActive(true);
                Bannanas[1].SetActive(true);
                Bannanas[2].SetActive(true);
                Bannanas[3].SetActive(true);
                Bannanas[4].SetActive(true);
                Bannanas[5].SetActive(true);
                Bannanas[6].SetActive(true);
                break;
        }
    }

    public void ShootBannana(GameObject g)  //shoot a single bananna prefab out from where the gun is
    {
        AmmoSingleBannana = Instantiate(g, transform.position + (transform.forward/4), Random.rotation);
        IC.Release(AmmoSingleBannana);
    }
    void SpawnBannanaGun(GameObject g) //spawn the bannana gun gets called at start
    {       
        BannanaGun = Instantiate(g, transform.position + (transform.forward/4), transform.rotation * Quaternion.Euler(0, 90,0));
        IC.ChildObject(BannanaGun);
    }

}
