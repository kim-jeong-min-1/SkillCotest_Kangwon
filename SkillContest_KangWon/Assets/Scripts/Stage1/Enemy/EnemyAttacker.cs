using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private Enemy enemyObject;

    public void EnemyAttackTrigger()
    {
        enemyObject.target.GetComponent<IDamagable>().ApplyDamage(enemyObject.enemyDamage);
    }
}
