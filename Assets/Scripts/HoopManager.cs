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

    public float timer;
    public int comboTime;

    public int LastHoopLocationRef;

    public bool FirstHoop = true;
    public bool finishLoop;

    IEnumerator InstantiateHoops()
    {
       
          for (int i = HoopsInScene; i < MaxHoopsInScene; i++)
          {           
                print("In Loop");
                int a = ChooseHoopSpawn(LastHoopLocationRef);
                Instantiate(HoopPrefab, HoopSpawnLocations[a].position, Quaternion.Euler(90, 0, 0));
                HoopsInScene++;
                LastHoopLocationRef = i;
                i = HoopsInScene;
                yield return new WaitForSeconds(1f);  
          }
        finishLoop = true;
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
        HoopTimer();
        if (finishLoop) { StartCoroutine(InstantiateHoops()); };
    }

   int ChooseHoopSpawn(int i)
    {   
        int a = Random.Range(0,6);
        while (a == i) { a = Random.Range(0, 6); }
        return a;
    }

    void StartOver()
    { }

    void HoopTimer()
    {
        timer += Time.deltaTime;
    }

    public void TimerReset(GameObject a)
    {
        if (timer < comboTime && !FirstHoop) { Combo(); timer = 0; a.SetActive(true); }
        else { timer = 0; FirstHoop = false; }
    }
    void Combo()
    {
        print("Combo");
    }

}
