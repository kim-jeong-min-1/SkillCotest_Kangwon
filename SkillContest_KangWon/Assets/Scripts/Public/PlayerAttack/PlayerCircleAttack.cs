using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleAttack : PlayerAttacker
{
    [SerializeField] private Bullet circleBullet;
    [SerializeField] private int shotCount;

    public override void LevelUP()
    {
        base.LevelUP();

        attackDamage += damagePlusValue;
        attackDelay -= delayMinusValue;
        shotCount++;
    }

    public override void Attack()
    {
        curTime = 0;

        StartCoroutine(circle());
        IEnumerator circle()
        {
            for (int i = 0; i < shotCount; i++)
            {
                for (int jj = 0; jj < 360; jj += 360 / 30)
                {
                    Bullet bullet = Instantiate(circleBullet, firePos.position, Quaternion.Euler(0, jj, 0));
                    bullet.Damage = attackDamage;
                }

                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
