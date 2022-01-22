using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerFiller : MonoBehaviour
{
    [SerializeField] private Cannon cannon;

    [SerializeField] private Image bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bar.fillAmount = cannon.GetPower();
    }
}
