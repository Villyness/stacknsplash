using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance { get { return _instance; } }


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
        //need to work out how to make below sectioon read only in the inspector 
    [Header("Score the Player Sees")]
    public int GameScore;
    [Header("Detirmines Difficulty Level")]
    public int HiddenGameScore;
    [Header("Current Difficulty Level")]
    public int Level;
    [Space(10)]

    //the points needed to next level
    [Header("Game Level Settings")]
    [Header("Level 1")]
    public int Level1PenaltyAmount;
    [Header("Level 2")]
    public int LevelTwoScore;
    public int Level2PenaltyAmount;
    [Header("Level 3")]
    public int LevelThreeScore;
    public int Level3PenaltyAmount;
    [Header("Level 4")]
    public int LevelFourScore;
    public int Level4PenaltyAmount;
    [Header("Level 5")]
    public int LevelFiveScore;
    public int LevelFivePenaltyAmount;

    bool DoOnlyOnce = true; //the UpdateLevel() only gets called once anyway whenever a hoop is destroyed or expired, so we dont really need this bool
        //there are four stages that affect the spawn locations of the hoops and how random they are. 
        //after level 4 some variables of the hoop will change procedurally to give the impression of infinite levels 
        //the player can fall back in levels depending on the game score and if they miss hoops

        private void Start()
        {
            Level = 1;
            UpdateLevel();
        }
    private void Update()
    {
     
    }
    public void UpdateLevel()
        {
            if (HiddenGameScore < LevelTwoScore) { Level = 1; HoopManager.Instance.NewHoopLevel(DoOnlyOnce); }
            if (HiddenGameScore >= LevelTwoScore && HiddenGameScore < LevelThreeScore) { Level = 2; HoopManager.Instance.NewHoopLevel(DoOnlyOnce); }
            if (HiddenGameScore >= LevelThreeScore && HiddenGameScore < LevelFourScore) { Level = 3; HoopManager.Instance.NewHoopLevel(DoOnlyOnce); }
            if (HiddenGameScore >= LevelFourScore) { Level = 4; HoopManager.Instance.NewHoopLevel(DoOnlyOnce); }
            if (HiddenGameScore >= LevelFiveScore) { Level = 5; HoopManager.Instance.NewHoopLevel(DoOnlyOnce); }
    }

    public void AddGameScore()
    {

    }

    public void SubtractGameSCore()
    {
        switch (Level)
        {
            case 1:
                HiddenGameScore -= Level1PenaltyAmount;
                break;
            case 2:
                HiddenGameScore -= Level2PenaltyAmount;
                break;
            case 3:
                HiddenGameScore -= Level3PenaltyAmount;
                break;
            case 4:
                HiddenGameScore -= Level4PenaltyAmount;
                break;
            case 5:
                HiddenGameScore -= LevelFivePenaltyAmount;
                break;
        }

    }
}

