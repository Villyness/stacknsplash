using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplodyTriggers : MonoBehaviour
{
    //public GameObject OutPartSystemObject;

   // public GameObject UpPartSystemObject;

    public ParticleSystem UpPartSystem;

    public ParticleSystem OutPartSystem;

    public MeshRenderer HoopMesh;
   

    private void Start()
    {

    }

    public IEnumerator PartyExplosion()
    {
        HoopMesh.enabled = false;
        print("party");
         float Outtime = OutPartSystem.main.duration;
         OutPartSystem.Play();
         yield return new WaitForSeconds(Outtime);
        
         UpPartSystem.Play();
         float Uptime = UpPartSystem.main.duration;
         yield return new WaitForSeconds(Uptime);

        Destroy(this.gameObject);
        yield return null;
    }
}
