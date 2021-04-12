using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplodyTriggers : MonoBehaviour
{
    //public GameObject OutPartSystemObject;

   // public GameObject UpPartSystemObject;

    public ParticleSystem UpPartSystem;

    public ParticleSystem OutPartSystem;

    GameObject Shape;

    MeshRenderer ShapeRenderer;
   

    private void Awake()
    {
        //OutPartSystemObject = transform.GetChild(0).gameObject;
        //if(!OutPartSystemObject) print("hello");
       // OutPartSystem = OutPartSystemObject.GetComponent<ParticleSystem>();

        ///UpPartSystemObject = transform.GetChild(1).gameObject;
       // UpPartSystem = UpPartSystemObject.GetComponent<ParticleSystem>();

        Shape = transform.parent.gameObject;
        ShapeRenderer = Shape.GetComponent<MeshRenderer>();
    }

    public IEnumerator PartyExplosion()
    {
        ShapeRenderer.enabled = false;

        float Outtime = OutPartSystem.main.duration;
        OutPartSystem.Play();
        yield return new WaitForSeconds(Outtime);

        UpPartSystem.Play();
        float Uptime = UpPartSystem.main.duration;
        yield return new WaitForSeconds(Uptime);

        Destroy(Shape);
        yield return null;
    }
}
