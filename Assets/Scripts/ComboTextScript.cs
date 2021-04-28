using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTextScript : MonoBehaviour
{
    private void Start()
    {
        if (HoopManager.Instance.ComboCount > 1) { transform.GetChild(0).gameObject.SetActive(true); }
    }
}
