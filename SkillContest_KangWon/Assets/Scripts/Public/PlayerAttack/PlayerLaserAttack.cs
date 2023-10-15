using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaserAttack : PlayerAttacker
{
    [SerializeField] private LineRenderer laser;
    [SerializeField] private GameObject laserPoint;
    [SerializeField] private float duration;
    [SerializeField] private float durationPlusValue;
    private Enemy target;

    public override void LevelUP()
    {
        base.LevelUP();

        duration += durationPlusValue;
        attackDamage += damagePlusValue;
    }

    public override void Attack()
    {
        curTime = 0;
        StartCoroutine(LaserAttack());
    }

    private IEnumerator LaserAttack()
    {
        float curTime = 0;

        while (curTime < duration)
        {
            curTime += Time.deltaTime;

            var cols = Physics.OverlapSphere(firePos.position, attackRange, LayerMask.GetMask("Enemy"));
            if(cols.Length != 0)
            {
                target = cols[0].GetComponent<Enemy>();
            }

            if(target != null)
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(0, firePos.position);
                laser.SetPosition(1, target.transform.position + Vector3.up);

                target.ApplyDamage(attackDamage);
                laserPoint.transform.position = target.transform.position + Vector3.up;
            }
            else
            {
                laser.gameObject.SetActive(false);
            }

            yield return null;
        }
        laser.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePos.position, attackRange);
    }
}
