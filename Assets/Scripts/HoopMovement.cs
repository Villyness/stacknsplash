using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    public GameObject Player;         
    public HoopDisapear HDonThisHoop;   //the other script attached to this prefab that manages logic for destroying the hoop and effects for it 
    public float HoopVelocity;      //detirmines speed and direction around player
    public bool pingPong;           //will this hoop pinpong on the y axis 
    public int pingPongAmount;      //how much will it ping pong
    public float HoopSize;
    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HDonThisHoop = GetComponent<HoopDisapear>();
        switch (GameManager.Instance.Level)
        {
            //Lvl 1: hoops all same size, same speed(20) and move in the same direction. they have a lifetime of 20sec
            case 1:     
                HoopVelocity = 15;                  
                HDonThisHoop.MaxLifetime = 20;
                break;
                //Lvl 2: all hoops ping pong in the same direction with the same speed(20). 
                //50 % chance of hoops being smaller and their lifetime is now 15sec
            case 2:    
                pingPong = true;
                pingPongAmount = 5;
                HoopVelocity = 20;
                HDonThisHoop.MaxLifetime = 15;
                break;
                //Lvl 3: 50% chaance of ping pong. 50% of hoops being smaller and 50% of hoops being being between 1-2 of its size. 
                //hoop velocity is now 50% chance of either direction, with a speed between 20-40
            case 3:     
                if (Random.value < 0.5f) { pingPong = true; }
                pingPongAmount = Random.Range(3,8);
                if (Random.value < 0.5f) { HoopVelocity = Random.Range(-40, -20); }
                else { HoopVelocity = Random.Range(20, 40); }
                HDonThisHoop.MaxLifetime = 15;
                break;
                //Lvl 4:  50% chance of ping pong. 50% of hoops being smaller and 50% of hoops being being between 1-2 of its size.  
                //hoop velocity is now 50% chance of either direction, with a speed between 30-50
            case 4:   
                if (Random.value < 0.5f) { pingPong = true; }
                pingPongAmount = Random.Range(5, 10);
                if (Random.value < 0.5f) { HoopVelocity = Random.Range(-50, -30); }
                else { HoopVelocity = Random.Range(30, 50); }
                HDonThisHoop.MaxLifetime = 10;
                break;
        }
       
    }
    void Update()
    {
        transform.parent.localScale = new Vector3(HoopSize,HoopSize,HoopSize);
        transform.parent.RotateAround(Player.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);
        if (pingPong) { transform.parent.position = new Vector3(transform.parent.position.x, Mathf.PingPong(Time.time * 3, 5), transform.parent.position.z); }
    }


}
