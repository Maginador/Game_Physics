using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerFiller : MonoBehaviour
{
    [SerializeField] private Cannon cannon;

    [SerializeField] private Image bar;

    void Update()
    {
        bar.fillAmount = cannon.GetPower();
    }
}
