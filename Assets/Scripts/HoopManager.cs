using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    public GameObject HoopPrefab;
    public GameObject HoopSpawn;
    public Transform[] HoopSpawnLocations;
    public int MaxHoopsInScene;
    public int HoopsInScene;

    public int LastHoopLocationRef;


    IEnumerator InstantiateHoops()
    {
       
          for (int i = HoopsInScene; i < MaxHoopsInScene; i++)
          {           
                print("In Loop");
                int a = ChooseHoopSpawn(LastHoopLocationRef);
                Instantiate(HoopPrefab, HoopSpawnLocations[a].position, Quaternion.Euler(90, 90, 0));
                HoopsInScene++;
                LastHoopLocationRef = i;
                i = HoopsInScene;
                yield return new WaitForSeconds(1f);  
          }      
    }


    private void Start()
    {
        HoopSpawnLocations[0] = HoopSpawn.transform.GetChild(0);
        HoopSpawnLocations[1] = HoopSpawn.transform.GetChild(1);
        HoopSpawnLocations[2] = HoopSpawn.transform.GetChild(2);
        HoopSpawnLocations[3] = HoopSpawn.transform.GetChild(3);
        HoopSpawnLocations[4] = HoopSpawn.transform.GetChild(4);
        HoopSpawnLocations[5] = HoopSpawn.transform.GetChild(5);

        StartCoroutine(InstantiateHoops());
    }

    private void Update()
    {
        // StartCoroutine(InstantiateHoops());
        if (HoopsInScene < MaxHoopsInScene) { StartCoroutine(InstantiateHoops()); };
    }

   int ChooseHoopSpawn(int i)
    {        
        int a = Random.Range(0,6);
        while (a == i) { a = Random.Range(0, 6); }
        return a;
    }

    void StartOver()
    { }

}
