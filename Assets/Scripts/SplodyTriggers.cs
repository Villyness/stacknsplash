using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplodyTriggers : MonoBehaviour
{
    GameObject OutPartSystemObject;

    GameObject UpPartSystemObject;

    ParticleSystem UpPartSystem;

    ParticleSystem OutPartSystem;

    GameObject Shape;

    MeshRenderer ShapeRenderer;
   

    private void Start()
    {
        OutPartSystemObject = transform.GetChild(0).gameObject;
        OutPartSystem = OutPartSystemObject.GetComponent<ParticleSystem>();

        UpPartSystemObject = transform.GetChild(1).gameObject;
        UpPartSystem = UpPartSystemObject.GetComponent<ParticleSystem>();

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
