using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopCollisionTrigger : MonoBehaviour
{
    public HoopDisapear HD;
    public GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        HD = GetComponentInParent<HoopDisapear>();
    }

    private void OnTriggerEnter(Collider other)
    {
        HD.disapear = true;
        GM.GameScore++;
    }
}
