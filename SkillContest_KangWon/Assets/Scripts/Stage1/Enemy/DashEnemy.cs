using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : Enemy
{
    [SerializeField] private float dashPower;
    [SerializeField] private Rigidbody rb;
    private bool isDashing = false;

    protected override void EnemyAI_Update()
    {
        if (isCanAttack && !isDashing)
        {
            EnemyAttack();
            enemyAgnet.isStopped = true;
            isDashing = true;
        }
    }

    protected override void EnemyAttack()
    {
        var rand = Random.Range(0, 2);

        if (rand == 0)
        {
            var dir = (target.position - transform.position).normalized;
            transform.rotation = Quaternion.Euler(dir);

            StartCoroutine(Dash(dir));
        }
        else
        {
            var randRot = Random.Range(0, 360);
            var rot = new Vector3(0, randRot, 0);

            transform.rotation = Quaternion.Euler(rot);

            StartCoroutine(Dash(transform.forward));
        }
    }

    private IEnumerator Dash(Vector3 pos)
    {
        rb.AddForce(pos * dashPower, ForceMode.Impulse);

        yield return new WaitForSeconds(attackDelay);
        enemyAgnet.isStopped = false;
        isDashing = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {
            other.GetComponent<IDamagable>().ApplyDamage(enemyDamage);
        }
    }
}
