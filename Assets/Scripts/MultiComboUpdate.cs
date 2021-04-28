using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiComboUpdate : MonoBehaviour
{
    Text MultiComboInt;
    // Start is called before the first frame update
    void Start()
    {
        MultiComboInt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        MultiComboInt.text = HoopManager.Instance.ComboCount.ToString();
    }
}
