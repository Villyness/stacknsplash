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

    [Header("References Needed")]
    [Tooltip("Drag Hoop Prefab from Asset Folder not scene")]
    public GameObject HoopPrefab;   //the default hoop prefab
    [Tooltip("Drag from Scene")]
    public GameObject FrontHoopSpawn;    //The game objects in the scene that the hoop spawn locations are childed to 
    [Tooltip("Drag from Scene")]
    public GameObject BackHoopSpawn;
    [Space(10)]

    [Header("Combo")]
    public int ComboTimeLimit;   //if the player shoots a hoop while timer <= comboTime then they get a combo
    public enum Direction { AntiClockwise, Clockwise, Alternate}
    [Space(10)]

    [Header("Hoop Level Progression")]
    [Header("Spawn Locations are numbered from 0-5 in Front," +
        " and  6-11 Behind")]
    public int CurrentAmountHoops;    //current amount of hoops in scene (should be read only in inspector)
    [Space(1)]
    [Header("Level 1")]
    public Direction Level1Direction;
    public int Level1MaxHoops;
    public int Level1HoopLifeTime;
    public float Level1HoopSpawnDelay;
    public Vector2 Level1HoopvelocityMinMax;
    public int Level1ChanceOfPingPong;
    public Vector2 Level1HoopSizeMinMax;
    public bool[] Level1HoopLocationIndexes = new bool[12];
    [Header("Level 2")]
    public Direction Level2Direction;
    public int Level2MaxHoops;
    public int Level2HoopLifeTime;
    public float Level2HoopSpawnDelay;
    public Vector2 Level2HoopvelocityMinMax;
    public int Level2ChanceOfPingPong;
    public Vector2 Level2HoopSizeMinMax;
    public bool[] Level2HoopLocationIndexes = new bool[12];
    [Header("Level 3")]
    public Direction Level3Direction;
    public int Level3MaxHoops;
    public int Level3HoopLifeTime;
    public float Level3HoopSpawnDelay;
    public Vector2 Level3HoopvelocityMinMax;
    public int Level3ChanceOfPingPong;
    public Vector2 Level3HoopSizeMinMax;
    public bool[] Level3HoopLocationIndexes = new bool[12];
    [Header("Level 4")]
    public Direction Level4Direction;
    public int Level4MaxHoops;
    public int Level4HoopLifeTime;
    public float Level4HoopSpawnDelay;
    public Vector2 Level4HoopvelocityMinMax;
    public int Level4ChanceOfPingPong;
    public Vector2 Level4HoopSizeMinMax;
    public bool[] Level4HoopLocationIndexes = new bool[12];
    [Header("Level 5")]
    public Direction Level5Direction;
    public int Level5MaxHoops;
    public int Level5HoopLifeTime;
    public float Level5HoopSpawnDelay;
    public Vector2 Level5HoopvelocityMinMax;
    public int Level5ChanceOfPingPong;
    public Vector2 Level5HoopSizeMinMax;
    public bool[] Level5HoopLocationIndexes = new bool[12];


    Transform[] HoopSpawnLocations = new Transform[12];  //array of hoop Spawn locations
    float timer; //a timer that resets whenever a player shoots a hoop
    bool FirstHoop = true;   //this is a check so the player doesnt get a combo on their first ring
    bool finishLoop; //this is just to restart the hoop spawn loop if it finishes (without restarting it a bunch of times per second)

    //All the variables below are updated everytime the level is changed and some are updated with each hoop in the spawn loop. 
    //These variables determine the spawn delay, size, location, and velocity of each hoop in the for loop after being set from public variables in the inspector relevent to the level.
    public List<int> CurrentSpawnLocationIndexes;
    int CurrentLocationIndex = 0; //a reference to a position in the spawn location array from the last loop. this is just to avoid hoops spawning in the same spot twice
    GameObject CurrentHoop;
    int CurrentHoopSpawnLocationIndex;  //this gets updated in a switch statement checking what level we're on and is used when setting the position of a new hoop in the for loop
    int LastHoopSpawnLocationIndex;  //this gets updated in a switch statement checking what level we're on and is used when setting the position of a new hoop in the for loop
    public float CurrentHoopSpawnDelay; //amount in seconds the loop has to wait every cycle. can be used to space out how far apart the hoops will be
    int CurrentMaxHoopsInScene; //max amount of hoops in scene, if there is less that max, another hoop will spawn     
    int CurrentHoopMaxLifetime;
    Vector2 CurrentHoopVelocityMinMax;
    public float CurrentHoopVelocity; //should be read only in inspector
    Vector2 CurrentHoopSizeMinMax;
    float CurrentHoopSize;
    bool PingPong = false;
    bool AntiClockwise;
    Direction CurrentDirection;
    bool NewLevel = false;
    bool DoOnlyOnce = true;
    public int iChecker;
    public int iterations;

    bool ComboCheck;
    public int ComboCount;
    private void Start()
    {
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
        if (finishLoop) { finishLoop = false; StartCoroutine(InstantiateHoops()); }     //just making sure the hoop spawn loop is running
    }

    IEnumerator InstantiateHoops()
    {
        print("Instantiate Hoops");

        //this loop is instantiating a hoop prefab and setting its values that are influenced by the NewHoopLevel() below and the Game level
        if (CurrentAmountHoops < CurrentMaxHoopsInScene)
        {
            for (int i = 0; i < CurrentMaxHoopsInScene; i++)
            {
                iterations++;
                //note the "CurrentHoopSpawnLocationIndex" = the value of the element that is randomly picked from the CurrentSpawnLocationIndexes<> list and and not the indexes in the Random.Range() method
                CurrentHoopSpawnLocationIndex = AlternateSpawn(CurrentHoopSpawnLocationIndex);
                if (CurrentHoopSpawnLocationIndex < 6) {CurrentHoop = Instantiate(HoopPrefab, HoopSpawnLocations[CurrentHoopSpawnLocationIndex].position, Quaternion.Euler(90, 0, 0)); } //add dot product check here to fix combo backwards
                else { CurrentHoop = Instantiate(HoopPrefab, HoopSpawnLocations[CurrentHoopSpawnLocationIndex].position, Quaternion.Euler(90, 180, 0)); } //add dot product check here to fix combo backwards}
                    HoopMovement HM = CurrentHoop.GetComponentInChildren<HoopMovement>();
                HoopDisapear HD = CurrentHoop.GetComponentInChildren<HoopDisapear>();
                CurrentHoopSize = Random.Range(CurrentHoopSizeMinMax.x, CurrentHoopSizeMinMax.y); //Randomly selecting from the current min or max this hoop could spawn          
                CurrentHoopVelocity = Random.Range(CurrentHoopVelocityMinMax.x, CurrentHoopVelocityMinMax.y);
                if (CurrentHoopSpawnLocationIndex != 0 && CurrentHoopSpawnLocationIndex != 6) { HM.HoopSize = CurrentHoopSize;  } else { HM.HoopSize = 0.5f; CurrentHoopVelocity = 13; }
                HM.HoopVelocity = ChooseDirection(CurrentDirection, CurrentHoopVelocity);
               
                HM.pingPong = PingPong;
                HD.MaxLifetime = CurrentHoopMaxLifetime;
                CurrentAmountHoops++;
                CurrentLocationIndex = i;
                LastHoopSpawnLocationIndex = CurrentHoopSpawnLocationIndex;
                // i = CurrentAmountHoops;
                iChecker = i;
                // if (NewLevel && !FirstHoop) { NewLevel = false; break;  }
                yield return new WaitForSeconds(CurrentHoopSpawnDelay);
            }
        }
        iterations = 0;
        print("finishSpawnLoop");
        finishLoop = true;
    }

    public void NewHoopLevel(bool a) //this gets called once in the game manager everytime the hidden score goes over or under a trigger for level change
    {
        NewLevel = true;
        if (a)
        {
            a = false;
            switch (GameManager.Instance.Level)
            {
                case 1:
                    CurrentMaxHoopsInScene = Level1MaxHoops;
                    CurrentHoopMaxLifetime = Level1HoopLifeTime;
                    CurrentHoopSpawnDelay = Level1HoopSpawnDelay;
                    CurrentHoopVelocityMinMax = Level1HoopvelocityMinMax;
                    CurrentDirection = Level1Direction;
                    if (Level1ChanceOfPingPong >= Random.Range(0, 100)) { PingPong = true; } else { PingPong = false; }
                    CurrentHoopSizeMinMax = Level1HoopSizeMinMax;
                    AssignSpawnPointsToList(Level1HoopLocationIndexes);
                    break;
                case 2:
                    CurrentMaxHoopsInScene = Level2MaxHoops;
                    CurrentHoopMaxLifetime = Level2HoopLifeTime;
                    CurrentHoopSpawnDelay = Level2HoopSpawnDelay;
                    CurrentHoopVelocityMinMax = Level2HoopvelocityMinMax;
                    CurrentDirection = Level2Direction;
                    if (Level2ChanceOfPingPong >= Random.Range(0, 100)) { PingPong = true; } else { PingPong = false; }
                    CurrentHoopSizeMinMax = Level2HoopSizeMinMax;
                    AssignSpawnPointsToList(Level2HoopLocationIndexes);
                    break;
                case 3:
                    CurrentMaxHoopsInScene = Level3MaxHoops;
                    CurrentHoopMaxLifetime = Level3HoopLifeTime;
                    CurrentHoopSpawnDelay = Level3HoopSpawnDelay;
                    CurrentHoopVelocityMinMax = Level3HoopvelocityMinMax;
                    CurrentDirection = Level3Direction;
                    if (Level3ChanceOfPingPong >= Random.Range(0, 100)) { PingPong = true; } else { PingPong = false; }
                    CurrentHoopSizeMinMax = Level3HoopSizeMinMax;
                    AssignSpawnPointsToList(Level3HoopLocationIndexes);
                    break;
                case 4:
                    CurrentMaxHoopsInScene = Level4MaxHoops;
                    CurrentHoopMaxLifetime = Level4HoopLifeTime;
                    CurrentHoopSpawnDelay = Level4HoopSpawnDelay;
                    CurrentHoopVelocityMinMax = Level4HoopvelocityMinMax;
                    CurrentDirection = Level4Direction;
                    if ( Level4ChanceOfPingPong >= Random.Range(0, 100)) { PingPong = true; } else { PingPong = false; }
                    CurrentHoopSizeMinMax = Level4HoopSizeMinMax;
                    AssignSpawnPointsToList(Level4HoopLocationIndexes);
                    break;
                case 5:
                    CurrentMaxHoopsInScene = Level5MaxHoops;
                    CurrentHoopMaxLifetime = Level5HoopLifeTime;
                    CurrentHoopSpawnDelay = Level5HoopSpawnDelay;
                    CurrentHoopVelocityMinMax = Level5HoopvelocityMinMax;
                    CurrentDirection = Level5Direction;
                    if (Level5ChanceOfPingPong >= Random.Range(0, 100)) { PingPong = true; } else { PingPong = false; }
                    CurrentHoopSizeMinMax = Level5HoopSizeMinMax;
                    AssignSpawnPointsToList(Level5HoopLocationIndexes);
                    break;
            }          
        }
    }
    //gets called in the for loop in InstantiateHoops() after CurrentHoopVelocity has been set
    public int AlternateSpawn(int a)//this is just to make it so hoops dont spawn in the same spot twice in a row 
    {
        a = CurrentSpawnLocationIndexes[Random.Range(0, CurrentSpawnLocationIndexes.Count)];
        if (CurrentSpawnLocationIndexes.Count > 1) { while (a == LastHoopSpawnLocationIndex) { a = CurrentSpawnLocationIndexes[Random.Range(0, CurrentSpawnLocationIndexes.Count)]; } }
        return a;
    }
    int ChooseDirection(Direction a, float b)   //gets the chosen direction from the inspector and changes the players velocity
    {
        int i = (int)a;
        switch (i)
        {
            case 0:
                b *= -1;
                print("antclockwise");
                AntiClockwise = false;
                break;
            case 1:
                b *= 1;
                AntiClockwise = true;
                break;
            case 2:
                if (AntiClockwise) { b *= -1; AntiClockwise = false; } else { b *= 1; AntiClockwise = true; }
                break;
        }
        return Mathf.RoundToInt(b);
    }
    //gets called in NewLevel()
    void AssignSpawnPointsToList(bool[] a)  //gets the spawn points chosen for the level in the inspector and adds them to a list
    {
        CurrentSpawnLocationIndexes.Clear();
        for (int i = 0; i < a.Length-1; i++)
        {
            if (a[i] == true) { CurrentSpawnLocationIndexes.Add(i); }
        }
    }

    public void TimerReset(GameObject a)    //gets called in the "hoop disapear" script. resets timer to 0 and checks if it was a combo  
    {
        if (timer < ComboTimeLimit && !FirstHoop)
        {
          if (ComboCheck) { ComboCount++; }
          Combo();
          timer = 0;
          a.SetActive(true);
        }
        else { timer = 0; FirstHoop = false; ComboCount = 1; }
    }

    void Combo()    //combo stuff
    {
        GameManager.Instance.GameScore += (5* ComboCount);
    }

 
}
