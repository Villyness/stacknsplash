using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopDisapear : MonoBehaviour
{
    public HoopManager HoopManScript;
    public GameObject ManagerObject;
    public AudioManager AudioManScript;
    public AudioSource ChimePlayer;
    public bool disapear;
    public SplodyTriggers XplosionTrigger;
    public float ShrinkSpeed;
    public AnimationCurve curve;
    bool PlayedAudio;
    float Shrink;
    float evaluate = 0;
    // Start is called before the first frame update
    void Start()
    {
        XplosionTrigger = GetComponentInChildren<SplodyTriggers>();
        ManagerObject = GameObject.FindGameObjectWithTag("Manager");
        HoopManScript = ManagerObject.GetComponent<HoopManager>();
        AudioManScript = ManagerObject.GetComponentInChildren<AudioManager>();
        ChimePlayer = GetComponent<AudioSource>();
        ChimePlayer.clip = AudioManScript.HoopChimes[Random.Range(0, 7)];
    }
    private void FixedUpdate()
    {
        if (disapear)
        {
            PlayAudio(PlayedAudio);
            evaluate += 0.1f;
            evaluate = Mathf.Clamp(evaluate, 0f, 1f);
            Shrink = curve.Evaluate(evaluate);                  
            transform.eulerAngles += new Vector3(10,0,0);
            ScaleDown();
            if (transform.localScale.x < 0.5) { StartCoroutine(XplosionTrigger.PartyExplosion()); }
        }
    }

    void PlayAudio(bool AudioPlay)
    {
        if (AudioPlay == false)
        {
            GetComponent<AudioSource>().clip = ChimePlayer.clip;
            GetComponent<AudioSource>().Play();
            PlayedAudio = true;
        }
    }

    // Update is called once per frame

  

    void ScaleDown()
    {
        transform.localScale = transform.localScale - new Vector3(Shrink,0,Shrink); 
    }
}
