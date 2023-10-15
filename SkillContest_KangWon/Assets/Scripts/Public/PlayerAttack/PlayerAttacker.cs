using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttacker : MonoBehaviour
{
    public int maxLevel;
    public float attackDelay;
    public float attackDamage;
    public PlayerAttackType type;
    public int Level = 0;

    [SerializeField] protected float attackRange;
    [SerializeField] protected float damagePlusValue;
    [SerializeField] protected float delayMinusValue;
    [SerializeField] protected Transform firePos;
    protected float curTime = 0;

    private void Start()
    {
        curTime = attackDelay;
    }

    public abstract void Attack();

    private void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= attackDelay)
        {
            Attack();
        }
    }

    public virtual void LevelUP()
    {
        if(Level < maxLevel)
        {
            Level++;
        }
    }

    public void SetAttacker()
    {
        if (!PlayerController.Inst.playerAttackers.ContainsKey(type))
        {
            PlayerController.Inst.playerAttackers.Add(type, this);
        }
    }
}

public enum PlayerAttackType
{
    Default,
    Laser,
    Barrel,
    Circle,
    Bounce,
    Missile,
    Boom
}
