using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMovement : MonoBehaviour
{
    public GameObject Player;
    public HoopDisapear HDonThisHoop;
    public float HoopVelocity;
    public float Range;
    public float changetimer;
    public bool changedFirstTime = false;
    public bool pingPong;
    public int pingPongAmount;
    public float HoopSize;
    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        HDonThisHoop = GetComponent<HoopDisapear>();
        switch (GameManager.Instance.Level)
        {
            case 1:
                HoopVelocity = 15;
                HoopSize = 1;
                HDonThisHoop.MaxLifetime = 20;
                break;
            case 2:
                pingPong = true;
                pingPongAmount = 5;
                HoopVelocity = 20;
                if (Random.value < 0.5f) { HoopSize = Random.Range(0.5f, 1); }
                HDonThisHoop.MaxLifetime = 15;
                break;
            case 3:
                if (Random.value < 0.5f) { pingPong = true; }
                pingPongAmount = Random.Range(3,8);
                if (Random.value < 0.5f) HoopSize = 0.5f;
                else HoopSize = Random.Range(1, 2);
                if (Random.value < 0.5f) { HoopVelocity = Random.Range(-40, -20); }
                else { HoopVelocity = Random.Range(20, 40); }
                HDonThisHoop.MaxLifetime = 15;
                break;
            case 4:
                if (Random.value < 0.5f) { pingPong = true; }
                pingPongAmount = Random.Range(5, 10);
                if (Random.value < 0.5f) HoopSize = 0.5f;
                else HoopSize = Random.Range(1, 2);
                if (Random.value < 0.5f) { HoopVelocity = Random.Range(-50, -30); }
                else { HoopVelocity = Random.Range(30, 50); }
                HDonThisHoop.MaxLifetime = 10;
                break;
        }
        transform.localScale = transform.localScale* HoopSize;
    }
    void Update()
    {
        transform.RotateAround(Player.transform.position, Vector3.up, HoopVelocity * Time.deltaTime);
        if (pingPong) { transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 3, 5), transform.position.z); }
    }


}
