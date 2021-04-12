using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShapeSpawner : MonoBehaviour
{
    public InteractionController IC;
    // public int ListDirection;
    // public GameObject[] Shapes;
    public GameObject BannanaGunPrefab;
    public GameObject SingleBannanaPrefab;
    GameObject BannanaGun;
    public GameObject AmmoSingleBannana;
   // public GameObject GunSingleBannana;
    public int ShapeDistanceFrom;
    public float CurrentShapeNo = 0;
    public int NextShapeNumber;
    public Vector3 ShapePos;
    public GameObject[] Bannanas;
    // public enum AmmoStates { OneBannana, TwoBannana, ThreeBannana, FiveBannanna, SixBananna }
    //AmmoStates BannanaAmmo;
    // Start is called before the first frame update
    void Start()
    {
        //CurrentShape = Instantiate(Shapeprefab, IC.pos[1], Quaternion.identity);
        //  NewShape();
        SpawnBannanaGun(BannanaGunPrefab);
        // foreach (int i = 0; i < BannanaGun.transform.childCount; i++)
        // {
        //     Bannanas[i-1] = BannanaGun.transform.GetChild(i).gameObject;
        // }
        Bannanas[0] = BannanaGun.transform.GetChild(0).gameObject; 
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

        switch (IC.Ammo)
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
        //  ShapePos = IC.Beam.GetPosition(1);
        // NewShape();
        // CurrentShape = Shapeprefab;
    }
  //  public void BannanaShooting()
   // {
   //
   // }

    public void NewShape()
    {

        print("test");
        ShootBannana(BannanaGunPrefab);
        IC.holdingShapeObject = AmmoSingleBannana;
        IC.ChildObject(AmmoSingleBannana);

        //   NextShapeNumber = Mathf.RoundToInt(CurrentShapeNo);

    }
    



    // public IEnumerator ShapeControllerInput(float a) //this gets called in the right & left button controller scripts 
    // {
    //     if (a < 0 && CurrentShapeNo > 0) { CurrentShapeNo = CurrentShapeNo - 0.1f; }
    //     if (a > 0 && CurrentShapeNo < 3) { CurrentShapeNo = CurrentShapeNo + 0.1f; }
    //     if (a > 0 && CurrentShapeNo >= 3) { CurrentShapeNo = 0; }
    //     if (a < 0 && CurrentShapeNo <= 0) { CurrentShapeNo = 3; }
    //     yield return new WaitForSeconds(0.1f);
    // }

    public void ShootBannana(GameObject g)
    {
        AmmoSingleBannana = Instantiate(g, transform.position + (transform.forward/4), Random.rotation);
        IC.Release(AmmoSingleBannana);
    }
    void SpawnBannanaGun(GameObject g)
    {       
        BannanaGun = Instantiate(g, transform.position + (transform.forward/4), transform.rotation * Quaternion.Euler(0, 90,0));
        IC.ChildObject(BannanaGun);
    }

}
