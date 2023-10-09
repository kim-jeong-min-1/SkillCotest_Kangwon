using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1Manager : StageManager
{
    [Header("Wave")]
    [SerializeField] private Text waveTimeText;
    [SerializeField] private float waveTime;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    [SerializeField] private float spawnTimeMinusValue;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> spawnPoints;
    private float curTime;

    private void Start()
    {
        StartCoroutine(WaveTimeUpdate());
        StartCoroutine(Wave());
    }

    private IEnumerator WaveTimeUpdate()
    {
        while (curTime < waveTime)
        {
            curTime += Time.deltaTime;

            waveTimeText.text = $"{(int)curTime / 60} : {(int)curTime % 60}  /  2 : 00";
            yield return null;
        }

        isBoss = true;
    }

    private IEnumerator Wave()
    {
        while (!isBoss)
        {
            var randEnemy = Random.Range(0, 100);
            var randTime = Random.Range(minSpawnTime, maxSpawnTime);
            var randPoint = Random.Range(0, spawnPoints.Count);

            SpawnEnemy(randEnemy, randPoint);

            yield return new WaitForSeconds(randTime);

            minSpawnTime -= spawnTimeMinusValue;
            maxSpawnTime -= spawnTimeMinusValue;
        }
    }

    private void SpawnEnemy(int randEnemy, int randPoint)
    {
        Enemy enemy = null;
        switch(randEnemy)
        {
            case int i when i < 30 :
                enemy = enemies[0];
                break;
            case int i when i < 60:
                enemy = enemies[1];
                break;
            case int i when i < 80:
                enemy = enemies[2];
                break;
            case int i when i < 95:
                enemy = enemies[3];
                break;
            case int i when i < 100:
                enemy = enemies[4];
                break;
        }

        var point = spawnPoints[randPoint];
        var enemyObject = Instantiate(enemy, point.position, Quaternion.identity);
        enemyList.Add(enemyObject);
    }
}
