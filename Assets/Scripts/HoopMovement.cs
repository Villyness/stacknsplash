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
    }
    void Update()
    {
        transform.parent.localScale = new Vector3(HoopSize,HoopSize,HoopSize);
        transform.parent.RotateAround(Player.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);
        if (pingPong) { transform.parent.position = new Vector3(transform.parent.position.x, Mathf.PingPong(Time.time * 3, 5), transform.parent.position.z); }
    }


}
