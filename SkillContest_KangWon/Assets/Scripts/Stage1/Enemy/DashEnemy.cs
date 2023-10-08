using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;

    protected override void EnemyAI_Update()
    {
        if (isCanAttack)
        {
            curAttackTime += Time.deltaTime;
            if (curAttackTime >= attackDelay)
            {
                EnemyAttack();
                curAttackTime = 0;
                enemyAgnet.isStopped = true;
            }
        }
    }

    protected override void EnemyAttack()
    {
        var rand = Random.Range(0, 2);

        if(rand == 0)
        {
            var dis = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dis, Vector3.up);

            StartCoroutine(Dash(target.position));
        }
        else
        {
            var randRot = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, randRot, 0);

            var pos = transform.forward * dashPower;
            StartCoroutine(Dash(pos));
        }
    }

    private IEnumerator Dash(Vector3 pos)
    {
        float curTime = 0;
        float percent = 0;
        Vector3 startPos = transform.position;

        while (percent < 1)
        {
            curTime += Time.deltaTime;
            percent = curTime / dashTime;

            transform.position = Vector3.Lerp(startPos, pos, percent);
            yield return null;
        }
        enemyAgnet.isStopped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.collider.GetComponent<IDamagable>().ApplyDamage(enemyDamage);
        }
    }
}
