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
        public int GameScore;
        public int HiddenGameScore; 
        public int Level;
        //the points needed to next level
        public int LevelTwoScore; 
        public int LevelThreeScore;
        public int LevelFourScore;

        //there are four stages that affect the spawn locations of the hoops and how random they are. 
        //after level 4 some variables of the hoop will change procedurally to give the impression of infinite levels 
        //the player can fall back in levels depending on the game score and if they miss hoops

        private void Start()
        {
            Level = 1;
        }

        public void UpdateLevel()
        {
            if (HiddenGameScore < LevelTwoScore) { Level = 1; }
            if (HiddenGameScore >= LevelTwoScore && GameScore < LevelThreeScore) { Level = 2; }
            if (HiddenGameScore >= LevelThreeScore && GameScore < LevelFourScore) { Level = 3; }
            if (HiddenGameScore >= LevelFourScore) { Level = 4; }
        }

    public void UpdateGameScoreUI()
    {

    }
}

