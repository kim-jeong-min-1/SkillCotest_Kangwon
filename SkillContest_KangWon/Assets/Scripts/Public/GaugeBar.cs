using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField] private Image gauge;
    private float maxGauge;
    private float curGauge;

    public void Init(float _maxGauge)
    {
        maxGauge = _maxGauge;
        curGauge = maxGauge;
        gauge.fillAmount = maxGauge/ maxGauge;
    }

    public void SetGauge(float curGauge)
    {
        this.curGauge = curGauge;
        gauge.fillAmount = curGauge / maxGauge;
    }

    public float GetCurGauge() => curGauge;
}
