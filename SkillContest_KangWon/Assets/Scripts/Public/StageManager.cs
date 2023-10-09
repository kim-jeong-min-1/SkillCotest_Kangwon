using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Inst { get; private set; }
    protected virtual void Awake() => Inst = this;

    public bool isGameClear { get; set; } = false;
    public bool isGameOver { get; set; } = false;
    public bool isBoss { get; set; } = false;

    public int Score { get; set; } = 0;

    [NonSerialized]
    public List<Enemy> enemyList = new List<Enemy>();
    public List<Enemy> GetCurEnemys() => enemyList;

    [SerializeField] private Text scoreText;

    public void AddScore(int score)
    {
        Score += score;
        scoreText.text = $"{Score}";
    }
}
