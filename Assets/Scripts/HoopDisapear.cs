using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopDisapear : MonoBehaviour
{
    public bool disapear;
    public SplodyTriggers XplosionTrigger;
    public float ShrinkSpeed;
    public AnimationCurve curve;
    float Shrink;
    float evaluate = 0;
    // Start is called before the first frame update
    void Start()
    {
        XplosionTrigger = GetComponentInChildren<SplodyTriggers>();
    }
    private void FixedUpdate()
    {
        if (disapear)
        {           
            evaluate += 0.1f;
            evaluate = Mathf.Clamp(evaluate, 0f, 1f);
            Shrink = curve.Evaluate(evaluate);
            ScaleDown();
            transform.eulerAngles += new Vector3(10,0,0);
            if (transform.localScale.x < 0.5) { StartCoroutine(XplosionTrigger.PartyExplosion()); }
        }
    }
    // Update is called once per frame

  

    void ScaleDown()
    {
        transform.localScale = transform.localScale - new Vector3(Shrink,0,Shrink); 
    }
}
