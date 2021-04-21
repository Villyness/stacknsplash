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

    //This script should really be called "HoopSpawnManager" . It handles where the hoops are going to spawn in the scene this changes depending on the level the player is at 

    public GameObject HoopPrefab;   //the default hoop prefab
    public GameObject CurrentHoop;

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
            CurrentHoop = Instantiate(HoopPrefab, HoopSpawnLocations[a].position, Quaternion.Euler(90, 0, 0));
            HoopSizeAndSpeed(CurrentHoop,HoopSpawnLocationIndex);
            HoopsInScene++;
            LastHoopLocationRef = i;
            i = HoopsInScene;
            yield return new WaitForSeconds(HoopSpawnDelay);
        }
        finishLoop = true;
    }

    void CheckSpawnLocation() //this decides how the hoops will spawn taking the level into accoutn
    {
        switch (GameManager.Instance.Level)
        {
            case 1:
                MaxHoopsInScene = 12;
                HoopSpawnLocationIndex = Level1Spawn();
                break;
            case 2:
                MaxHoopsInScene = 12;
                HoopSpawnLocationIndex = Level2Spawn(LastHoopLocationRef);
                HoopSpawnDelay = 0.5f;
                break;
            case 3:
                MaxHoopsInScene = 15;
                HoopSpawnLocationIndex = Level3Spawn(LastHoopLocationRef);
                break;
            case 4:
                MaxHoopsInScene = 15;
                HoopSpawnLocationIndex = Level4Spawn(LastHoopLocationRef);
                break;
        }
    }


    void HoopSizeAndSpeed(GameObject a, int b) //this decides the hoop prefabs velocity and size depending on what lane it will spawn in
    {
        HoopDisapear DisappearScript = a.GetComponentInChildren<HoopDisapear>();
        HoopMovement MovementScript = a.GetComponentInChildren<HoopMovement>();
        if (DisappearScript == null) { print("null"); }
        switch (b)
        {
            case 0: //lane 1 
                MovementScript.HoopSize = 0.5f;
                MovementScript.pingPong = true;
                break;
            case 1: //lane 2
                MovementScript.HoopSize = 0.8f;
                MovementScript.pingPong = false;
                break;
            case 2: //lane 3
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 3: //lane 4
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 4: //lane 5
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 5: //lane 6
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 6: //lane 1
                MovementScript.HoopSize = 0.5f;
                MovementScript.pingPong = true;
                break;
            case 7: //lane 2
                MovementScript.HoopSize = 0.8f;
                MovementScript.pingPong = false;
                break;
            case 8: //lane 3
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 9: //lane 4
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 10:   //lane 5
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;
            case 11:    //lane 6
                MovementScript.HoopSize = 1;
                MovementScript.pingPong = false;
                break;

        }
    }

    public void TimerReset(GameObject a)    //gets called in the "hoop disapear" script. resets timer to 0 and checks if it was a combo  
    {
        if (timer < comboTime && !FirstHoop) { Combo(); timer = 0; a.SetActive(true); }
        else { timer = 0; FirstHoop = false; }
    }

    void Combo()    //combo stuff
    {
        GameManager.Instance.GameScore += 5;
    }

 
    //these are all the int()s will be an idex in spawn transforms[] that is used to detirmine the hoops transform position in its spawn loop
    int Level1Spawn()   //lvl 1 is meant to be more uniform and the rings will all spawn in the same "lane" in front and behind the player 
    {
        int a;
        if (HoopSpawnLocationIndex ==1) { a = 7;}
        else { a = 1;  }
        return a;
    }
    int Level2Spawn(int i)   //lvl 2 the rings will still spawn in the same one lane 
    {
        int a;
        if (HoopSpawnLocationIndex == 1) { a = 7; }
        else { a = 1; }
        return a;   
    }
    int Level3Spawn(int i) //lvl 3 the rings will spawn between lanes 0-3 in front and behind 
    {
        int a;
        if (Random.value < .5) { a = Random.Range(0, 3); }     //50% chance of in front or behind
        else { a = Random.Range(6, 9); }  //choosing a random spot in the array
        return a;
    }
    int Level4Spawn(int i) //level 4 rings can spawn in any lane
    {
        int a;
        if (Random.value < .5) { a = Random.Range(0, 5); }     //50% chance of in front or behind
        else { a = Random.Range(0, 11); } //choosing a random spot in the array
        return a;
    } 
}
