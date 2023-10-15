using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float damage;
    [SerializeField] private int bounceCount;
    private Enemy target;
    private Enemy preTarget;
    private int curCount = 0;

    public void SetArrow(float _damage, int count)
    {
        damage = _damage;
        bounceCount = count;
        StartCoroutine(Arrow());
    }

    private IEnumerator Arrow()
    {
        while (curCount < bounceCount)
        {
            transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));

            if(target == null)
            {
                var cols = Physics.OverlapSphere(transform.position, attackRange, LayerMask.GetMask("Enemy"));

                if(cols.Length != 0)
                {
                    target = cols[0].GetComponent<Enemy>();

                    if(target == preTarget)
                    {
                        if(cols.Length >= 2)
                        {
                            target = cols[1].GetComponent<Enemy>();
                        }
                        else
                        {
                            target = null;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            if(target != null)
            {
                var dir = target.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(dir);
            }

            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            if(target != null && enemy == target)
            {
                curCount++;
                target.ApplyDamage(damage);
                preTarget = target;
                target = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
