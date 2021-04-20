using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopManager : MonoBehaviour
{
    private static HoopManager _instance;

    public static HoopManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject HoopPrefab;   //the default hoop prefab

    public int MaxHoopsInScene; //max amount of hoops in scene, if there is less that max, another hoop will spawn
    public int HoopsInScene;    //current amount of hoops in scene
    
    public GameObject FrontHoopSpawn;    //The game objects in the scene that the hoop spawn locations are childed to 
    public GameObject BackHoopSpawn;   
    public Transform[] HoopSpawnLocations;  //array of hoop Spawn locations
    public int HoopSpawnLocationIndex;  //this gets updated in a switch statement checking what level we're on and is used when setting the position of a new hoop in the for loop
    public int LastHoopLocationRef = 0; //a reference to a position in the spawn location array from the last loop. this is just to avoid hoops spawning in the same spot twice

    public float HoopSpawnDelay; //amount in seconds the loop has to wait every cycle. can be used to space out how far apart the hoops will be
    public float timer; //a timer that resets whenever a player shoots a hoop
    public int comboTime;   //if the player shoots a hoop while timer <= comboTime then they get a combo
 
    public bool FirstHoop = true;   //this is a check so the player doesnt get a combo on their first ring
    public bool finishLoop; //this is just to restart the hoop spawn loop if it finishes (without restarting it a bunch of times per second)

    private void Start()
    {
        HoopSpawnLocationIndex = Level1Spawn();
        //in front of player
        HoopSpawnLocations[0] = FrontHoopSpawn.transform.GetChild(0);    //the hoop spawn locations are game objects in the scene 
        HoopSpawnLocations[1] = FrontHoopSpawn.transform.GetChild(1);    //there might have been a less ugly way to assign them but this works :P
        HoopSpawnLocations[2] = FrontHoopSpawn.transform.GetChild(2);
        HoopSpawnLocations[3] = FrontHoopSpawn.transform.GetChild(3);   
        HoopSpawnLocations[4] = FrontHoopSpawn.transform.GetChild(4);
        HoopSpawnLocations[5] = FrontHoopSpawn.transform.GetChild(5);
        //behind player
        HoopSpawnLocations[6] = BackHoopSpawn.transform.GetChild(0);    
        HoopSpawnLocations[7] = BackHoopSpawn.transform.GetChild(1);    
        HoopSpawnLocations[8] = BackHoopSpawn.transform.GetChild(2);    
        HoopSpawnLocations[9] = BackHoopSpawn.transform.GetChild(3);
        HoopSpawnLocations[10] = BackHoopSpawn.transform.GetChild(4);
        HoopSpawnLocations[11] = BackHoopSpawn.transform.GetChild(5);

        StartCoroutine(InstantiateHoops());
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (finishLoop) { StartCoroutine(InstantiateHoops()); }     //just making sure the hoop spawn loop is running
    }

    IEnumerator InstantiateHoops()
    {
        //I did it in a for loop rather than a while loop as it seemed easier to control the amount of hoops that would spawn 
        //this just means i need to restart the loop if the conditions in for() are met. 
        for (int i = HoopsInScene; i < MaxHoopsInScene; i++)
        {
            CheckSpawnLocation();
            int a = HoopSpawnLocationIndex;
            Instantiate(HoopPrefab, HoopSpawnLocations[a].position, Quaternion.Euler(90, 0, 0));
            HoopsInScene++;
            LastHoopLocationRef = i;
            i = HoopsInScene;
            yield return new WaitForSeconds(HoopSpawnDelay);
        }
        finishLoop = true;
    }

    public void TimerReset(GameObject a)    //gets called in the "hoop disapear" script. resets timer to 0 and checks if it was a combo  
    {
        if (timer < comboTime && !FirstHoop) { Combo(); timer = 0; a.SetActive(true); }
        else { timer = 0; FirstHoop = false; }
    }

    void Combo()    //combo stuff
    {
        print("Combo");
    }

    void CheckSpawnLocation()
    {
        switch (GameManager.Instance.Level)
        {
            case 1:
                MaxHoopsInScene = 12;
                HoopSpawnLocationIndex = Level1Spawn();
                break;
            case 2:
                HoopSpawnLocationIndex = Level2Spawn(LastHoopLocationRef);
                break;
            case 3:
                HoopSpawnLocationIndex = Level3Spawn(LastHoopLocationRef);
                break;
            case 4:
                HoopSpawnLocationIndex = Level4Spawn(LastHoopLocationRef);
                break;
        }
    }

    //these are all the int()s will be an idex in spawn transforms[] that is used to detirmine the hoops transform position in its spawn loop
    int Level1Spawn()   //the first stage is meant to be more uniform and the rings will all spawn in the same "lane" in front and behind the player 
    {
        int a;
        if (HoopSpawnLocationIndex ==1) { a = 7;}
        else { a = 1;  }
        return a;
    }
    int Level2Spawn(int i)   //the second stage 
    {
        int a;
        if (HoopSpawnLocationIndex == 1) { a = 7; }
        else { a = 1; }
        return a;   
    }
    int Level3Spawn(int i) 
    {
        int a;
        if (Random.value < .5) { a = Random.Range(0, 3); }     //50% chance of in front or behind
        else { a = Random.Range(6, 9); }  //choosing a random spot in the array
        return a;
    }
    int Level4Spawn(int i) 
    {
        int a;
        if (Random.value < .5) { a = Random.Range(0, 5); }     //50% chance of in front or behind
        else { a = Random.Range(0, 11); } //choosing a random spot in the array
        return a;
    } 
}
