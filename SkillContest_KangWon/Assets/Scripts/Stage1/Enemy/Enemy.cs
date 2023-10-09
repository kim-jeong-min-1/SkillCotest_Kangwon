using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    [System.NonSerialized] public Transform target;
    [System.NonSerialized] public float enemyDamage;

    [SerializeField] protected EnemyStatus enemyStatus;
    [SerializeField] protected GaugeBar enemyHpBar;
    [SerializeField] protected Animator enemyAnimator;
    [SerializeField] protected float attackRadius;
    [SerializeField] protected int enemyScore;

    protected NavMeshAgent enemyAgnet;
    protected LayerMask targetLayer;
    protected float enemyHp;
    protected float enemyMaxHp;
    protected float enemySpeed;
    protected float attackDelay;
    protected float curAttackTime;
    protected bool isEnemyDie;
    protected bool isCanAttack;

    public void Awake()
    {
        EnemyInit(enemyStatus);
    }
    public void EnemyInit(EnemyStatus enemyStatus)
    {
        enemyHp = enemyStatus.enemyHp;
        enemySpeed = enemyStatus.enemySpeed;
        enemyDamage = enemyStatus.enemyDamage;
        attackDelay = enemyStatus.enemyAttackDelay;
        enemyMaxHp = enemyHp;
        curAttackTime = attackDelay;

        enemyHpBar.Init(enemyMaxHp);
        targetLayer = LayerMask.GetMask("Player");
        target = FindObjectOfType<PlayerController>().transform;
        enemyAgnet = GetComponent<NavMeshAgent>();

        enemyAgnet.speed = enemySpeed;
        enemyAgnet.stoppingDistance = attackRadius;
    }

    protected virtual void Update()
    {
        isCanAttack = AttackRangeCheck();
        enemyAgnet.SetDestination(target.position);

        EnemyAI_Update();
    }

    protected virtual void EnemyAI_Update()
    {
        if (isEnemyDie) return;

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
        else
        {
            enemyAgnet.isStopped = false;
        }
    }

    protected virtual void EnemyAttack()
    {
        target.GetComponent<IDamagable>().ApplyDamage(enemyDamage);
    }
    
    public virtual void EnemyDie()
    {
        enemyAnimator.SetTrigger("Die");
        var delay = enemyAnimator.GetCurrentAnimatorStateInfo(0).speed;

        StageManager.Inst.enemyList.Remove(this);
        StageManager.Inst.AddScore(enemyScore);

        Destroy(gameObject, delay);
    }

    public void ApplyDamage(float damage)
    {
        enemyHp -= damage;
        enemyHp = Mathf.Clamp(enemyHp, 0, enemyMaxHp);
        enemyHpBar.SetGauge(enemyHp);

        if (enemyHp <= 0 && !isEnemyDie)
        {
            isEnemyDie = true;
            enemyAgnet.isStopped = true;
            EnemyDie();
        }
    }

    private bool AttackRangeCheck()
    {
        var targets = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);
        return (targets.Length == 0) ? false : true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

[System.Serializable]
public class EnemyStatus
{
    public float enemyHp;
    public float enemySpeed;
    public float enemyDamage;
    public float enemyAttackDelay;
}
