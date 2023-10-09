using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemy : Enemy
{
    [SerializeField] private GameObject boomEffect;
    [SerializeField] private Transform boomPos;

    protected override void EnemyAttack()
    {
        
    }

    protected override void EnemyAI_Update()
    {
        if(isCanAttack) EnemyDie();
    }

    public override void EnemyDie()
    {
        var cols = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

        foreach (var col in cols)
        {
            col.GetComponent<IDamagable>().ApplyDamage(enemyDamage);
        }

        Instantiate(boomEffect, boomPos.position, Quaternion.identity);

        StageManager.Inst.enemyList.Remove(this);
        StageManager.Inst.AddScore(enemyScore);
        Destroy(gameObject);
    }
}
