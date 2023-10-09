using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitEnemy : Enemy
{
    [SerializeField] private int splitCount;
    [SerializeField] private SplitEnemy splitObject;

    private Vector3[] spawnPos = new Vector3[4];
    private int maxSplitCount = 3;

    public override void EnemyDie()
    {
        if(splitCount < 3)
        {
            var rand = Random.Range(2, 5);

            spawnPos[0] = transform.position + Vector3.right;
            spawnPos[1] = transform.position + Vector3.left;
            spawnPos[2] = transform.position + Vector3.up;
            spawnPos[3] = transform.position + Vector3.down;

            for (int i = 0; i < rand; i++)
            {
                var dis = (target.position - transform.position).normalized;
                var dir = Quaternion.LookRotation(dis, Vector3.up);

                Instantiate(splitObject, spawnPos[i], dir);
            }
        }
        base.EnemyDie();
    }

}
