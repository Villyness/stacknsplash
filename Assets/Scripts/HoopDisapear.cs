using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopDisapear : MonoBehaviour
{
    public AudioSource ChimePlayer;
    public Material ExpireMaterial;
    public Color StartColour;
    public Color EndColour;
    public float Lifetime;
    public int MaxLifetime;

    public bool disapear;
    bool expired = false;
    public SplodyTriggers XplosionTrigger;
    public float ShrinkSpeed;
    public AnimationCurve curve;
    public GameObject ComboText;
    bool DoOnlyOnce;
    bool changeMat = false;
    bool smallenough = false;
    float Shrink;
    float evaluate = 0;
    // Start is called before the first frame update
    void Start()
    {   
        ChimePlayer = GetComponent<AudioSource>();
        ChimePlayer.clip = AudioManager.Instance.HoopChimes[Random.Range(0, 7)];
        ExpireMaterial = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        Lifetime += Time.deltaTime;
        ExpireMaterial.color = Color.Lerp(StartColour, EndColour, Lifetime/10);
        if (Lifetime > MaxLifetime) Expire(expired);
    }

    private void FixedUpdate()
    {
        if (disapear)
        {
            HoopDestroyed(DoOnlyOnce);
            //evaluate += 0.1f;
            //evaluate = Mathf.Clamp(evaluate, 1f, 0f);
            //Shrink = curve.Evaluate(evaluate);
            //transform.eulerAngles += new Vector3(10, 0, 0);
            ScaleDown(smallenough);
            if (transform.localScale.x < 0.3) { smallenough = true; StartCoroutine(XplosionTrigger.PartyExplosion()); print("getting small"); }
        }
   

    }

    void Expire(bool a)
    {
        if (!a) { HoopManager.Instance.HoopsInScene--; if (GameManager.Instance.HiddenGameScore > 0) GameManager.Instance.HiddenGameScore-=2; }
        GameManager.Instance.UpdateLevel();
        expired = true;
        evaluate += 0.1f;
        evaluate = Mathf.Clamp(evaluate, 0f, 1f);
        Shrink = curve.Evaluate(evaluate);
        transform.eulerAngles += new Vector3(10, 0, 0);
        ScaleDown(smallenough);
        if (transform.localScale.x < 0.5) { smallenough = true;  Destroy(transform.parent.gameObject); } 
    }
        
    void HoopDestroyed(bool a)
    {
        if (a == false)
        {
            GameManager.Instance.GameScore+=5;
            GameManager.Instance.HiddenGameScore += 5;
            GameManager.Instance.UpdateLevel();
            HoopManager.Instance.TimerReset(ComboText);
            HoopManager.Instance.HoopsInScene--;
            GetComponent<AudioSource>().clip = ChimePlayer.clip;
            GetComponent<AudioSource>().Play();
            this.DoOnlyOnce = true;
        }
    }

    void ScaleDown(bool a)
    {
        if (!a) { transform.localScale = transform.localScale - new Vector3(0.3f, 0, 0.3f); }
    }
}
