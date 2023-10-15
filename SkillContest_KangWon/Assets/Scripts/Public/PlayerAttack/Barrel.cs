using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : BezierObject
{
    [SerializeField] private float moveTime;
    [SerializeField] private float attackRange;
    [SerializeField] private float knockBackPower;
    [SerializeField] private GameObject boom;
    private float attackDamage;

    public void SetBarrel(float damage, Vector3 target)
    {
        BezierStart(moveTime, target);
        attackDamage = damage;

        StartCoroutine(BoomCheck());
    }

    private IEnumerator BoomCheck()
    {
        while (!base.isDone)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Instantiate(boom, transform.position, Quaternion.identity);

        var cols = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        if(cols.Length != 0)
        {
            foreach (var col in cols)
            {
                col.GetComponent<Enemy>().ApplyDamage(attackDamage);

                var dir = (col.transform.position - transform.position).normalized;
                col.GetComponent<Rigidbody>().AddForce(dir * knockBackPower, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
