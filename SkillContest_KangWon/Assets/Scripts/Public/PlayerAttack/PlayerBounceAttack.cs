using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBounceAttack : PlayerAttacker 
{
    [SerializeField] private ArrowBullet arrow;
    [SerializeField] private int bounceCount;

    public override void Attack()
    {
        curTime = 0;
        var arrow = Instantiate(this.arrow, firePos.position, Quaternion.identity);
        arrow.SetArrow(attackDamage, bounceCount);
    }

    public override void LevelUP()
    {
        base.LevelUP();

        attackDamage += damagePlusValue;
        attackDelay -= delayMinusValue;
        bounceCount += 1;
    }

}
