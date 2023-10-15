using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultAttack : PlayerAttacker
{
    [SerializeField] private Bullet playerDefaultBullet;
    
    public override void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            curTime = 0;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit, Mathf.Infinity);

            var mousePos = hit.point;
            var playerPos = firePos.position;

            var dis = (mousePos - playerPos).normalized;
            var rot = Quaternion.LookRotation(dis);
            ShotBullet(rot);
        }
    }

    public override void LevelUP()
    {
        base.LevelUP();

        attackDamage += damagePlusValue;
        attackDelay -= delayMinusValue;
    }

    private void ShotBullet(Quaternion rot)
    {
        Bullet bullet = Instantiate(playerDefaultBullet, firePos.position, rot);
        bullet.Damage = attackDamage;

        for (int i = 1; i < Level; i++)
        {
            Bullet bullet2 = Instantiate(playerDefaultBullet, firePos.position, rot);
            bullet2.transform.eulerAngles += Vector3.up * (8 * i);
            bullet2.Damage = attackDamage;

            Bullet bullet3 = Instantiate(playerDefaultBullet, firePos.position, rot);
            bullet3.transform.eulerAngles += Vector3.up * (-8 * i);
            bullet3.Damage = attackDamage;
        }
    }
}
