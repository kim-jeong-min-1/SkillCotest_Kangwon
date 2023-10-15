using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSkill : PlayerSkill
{
    [SerializeField] private Transform center;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private float radius;
    [SerializeField] private float damage;

    public override void UseSkill()
    {
        var cols = Physics.OverlapSphere(center.position, radius, LayerMask.GetMask("Enemy"));

        if(cols.Length != 0)
        {
            foreach (var col in cols)
            {
                col.GetComponent<IDamagable>().ApplyDamage(damage);
                Instantiate(effect, col.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center.position, radius);
    }
}
