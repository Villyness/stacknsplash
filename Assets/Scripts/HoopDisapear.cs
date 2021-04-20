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
    float Shrink;
    float evaluate = 0;
    // Start is called before the first frame update
    void Start()
    {
        ComboText = transform.GetChild(2).gameObject;
        XplosionTrigger = GetComponentInChildren<SplodyTriggers>();
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
            evaluate += 0.1f;
            evaluate = Mathf.Clamp(evaluate, 0f, 1f);
            Shrink = curve.Evaluate(evaluate);
            transform.eulerAngles += new Vector3(10, 0, 0);
            ScaleDown();
            if (transform.localScale.x < 0.5) { StartCoroutine(XplosionTrigger.PartyExplosion()); }
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
        ScaleDown();
        if (transform.localScale.x < 0.5) { Destroy(this.gameObject); } 
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

    // Update is called once per frame  

    void ScaleDown()
    {
        transform.localScale = transform.localScale - new Vector3(Shrink,0,Shrink); 
    }
}
