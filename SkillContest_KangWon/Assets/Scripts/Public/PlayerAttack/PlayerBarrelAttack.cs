using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrelAttack : PlayerAttacker
{
    [SerializeField] private int barrelCount;
    [SerializeField] private Barrel barrel;

    public override void LevelUP()
    {
        base.LevelUP();

        attackDamage += damagePlusValue;
        barrelCount++;
    }

    public override void Attack()
    {
        curTime = 0;
        var cols = Physics.OverlapSphere(firePos.position, attackRange, LayerMask.GetMask("Enemy"));

        if(cols.Length != 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (i + 1 > barrelCount) break;

                var barrel = Instantiate(this.barrel, firePos.position, Quaternion.identity);
                barrel.SetBarrel(attackDamage, cols[i].transform.position);
            }
        }
    }
}
