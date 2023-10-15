using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rskill : PlayerSkill
{
    [SerializeField] private Transform shield;
    [SerializeField] private float invinDuration;
    [SerializeField] private float shieldRotateSpeed;

    public override void UseSkill()
    {
        StartCoroutine(PlayerInvincible());
        StartCoroutine(Shield());
    }

    private IEnumerator PlayerInvincible()
    {
        shield.gameObject.SetActive(true);
        player.isInvincible = true;

        yield return new WaitForSeconds(invinDuration);

        shield.gameObject.SetActive(false);
        player.isInvincible = false;
    }

    private IEnumerator Shield()
    {
        float curTime = 0;
        float percent = 0;
        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / invinDuration;

            shield.transform.eulerAngles += Vector3.up * shieldRotateSpeed;

            yield return null;
        }
    }
}
