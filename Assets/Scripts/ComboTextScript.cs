using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboTextScript : MonoBehaviour
{
    public GameObject MultiCombo;
    private void OnEnable()
    {
        if (HoopManager.Instance.ComboCount > 1) { MultiCombo.SetActive(true); print("multi combo"); }
    }

}
