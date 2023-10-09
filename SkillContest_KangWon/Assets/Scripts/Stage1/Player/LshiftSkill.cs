using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LshiftSkill : PlayerSkill
{
    [SerializeField] private float skillDuration;
    [SerializeField] private float speedValue;
    public override void UseSkill()
    {
        StartCoroutine(PlayerSpeedUp());
    }

    private IEnumerator PlayerSpeedUp()
    {
        player.PlayerSpeedChange(speedValue);

        yield return new WaitForSeconds(skillDuration);

        player.PlayerSpeedChange(-speedValue);
    }
}
