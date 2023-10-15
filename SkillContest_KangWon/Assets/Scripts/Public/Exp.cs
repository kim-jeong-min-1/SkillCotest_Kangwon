using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : BezierObject
{
    [SerializeField] private float range;
    [SerializeField] private float time;
    [SerializeField] private float exp;

    private void Start()
    {
        StartCoroutine(check());
    }

    private IEnumerator check()
    {
        while (true)
        {
            var cols = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Player"));

            if(cols.Length != 0)
            {
                StartCoroutine(moveToPlayer());
                yield break;
            }
            yield return null;
        }
    }
    private IEnumerator moveToPlayer()
    {
        var target = PlayerController.Inst.transform.position;

        BezierStart(time, target);

        while (!isDone)
        {
            BezierUpdate(PlayerController.Inst.transform.position);
            yield return null;
        }

        PlayerLevel.Inst.AddExp(exp);
        Destroy(gameObject);
    }
}
