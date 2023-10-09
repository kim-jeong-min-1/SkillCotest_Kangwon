using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{
    [SerializeField] private GaugeBar mainGauge;
    [SerializeField] private GaugeBar tempGauge;
    [SerializeField] private float smoothTime;
    private float maxHp;
    private float smoothValue;
    private Coroutine curSmoothRoutine;

    public void Init(float maxHp)
    {
        mainGauge.Init(maxHp);
        tempGauge.Init(maxHp);

        this.maxHp = maxHp;
    }

    public void SetHpBar(float curHp)
    {
        mainGauge.SetGauge(curHp);
        smoothValue = curHp;

        if (curSmoothRoutine != null) StopCoroutine(curSmoothRoutine);

        curSmoothRoutine = StartCoroutine(SmoothTempHp());
    }

    public void SetHpBarOneSecond(float curHp)
    {
        mainGauge.SetGauge(curHp);
        tempGauge.SetGauge(curHp);
    }

    private IEnumerator SmoothTempHp()
    {
        float start = tempGauge.GetCurGauge();
        float end = smoothValue;

        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / smoothTime;

            var value = Mathf.Lerp(start, end, percent);
            tempGauge.SetGauge(value);

            yield return null;
        }
    }
}
