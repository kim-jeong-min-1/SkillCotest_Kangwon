using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : Singletone<PlayerLevel>
{
    [SerializeField] private GaugeBar playerLevelBar;
    [SerializeField] private int maxLevel;
    [SerializeField] private float levelGaugeMax;
    [SerializeField] private float levelMaxPlus;
    [SerializeField] private Animator levelUpAnimator;
    [SerializeField] private Text levelText;
    [SerializeField] private List<SelectUI> selectUIs;
    [SerializeField] private List<PlayerAttackInfo> attackInfos;
    [SerializeField] private List<PlayerAttacker> playerAttackers;
    private int curLevel = 1;
    private float curLevelGauge = 0;

    protected override void Awake()
    {
        base.Awake();
        playerLevelBar.Init(levelGaugeMax);
    }

    public void AddExp(float exp)
    {
        if(curLevel >= maxLevel)
        {
            return;
        }

        curLevelGauge += exp;
        curLevelGauge = Mathf.Clamp(curLevelGauge, 0, levelGaugeMax);
        playerLevelBar.SetGauge(curLevelGauge);

        if (curLevelGauge >= levelGaugeMax)
        {
            curLevelGauge = 0;
            levelGaugeMax += levelMaxPlus;
            playerLevelBar.Init(levelGaugeMax);
            levelText.text = $"Lv:   {++curLevel}";
            Time.timeScale = 0.1f;

            RandomAttackers();
            levelUpAnimator.SetTrigger("On");
        }
    }

    public void SelectEnd()
    {
        levelUpAnimator.SetTrigger("Off");
        Time.timeScale = 1;
    }

    private void RandomAttackers()
    {
        int preIndex = int.MaxValue;
        foreach (var item in selectUIs)
        {
            var random = Random.Range(0, playerAttackers.Count);
            if (random == preIndex) random++;

            preIndex = random;
            var attacker = playerAttackers[random];
            var attackerInfo = attackInfos[random];

            if (PlayerController.Inst.playerAttackers.ContainsKey(attackerInfo.type))
            {
                item.Init(attackerInfo.sprite, attackerInfo.name, attackerInfo.explain, PlayerController.Inst.
                    playerAttackers[attackerInfo.type].Level, attacker);
            }
            else
            {
                item.Init(attackerInfo.sprite, attackerInfo.name, attackerInfo.explain, 0, attacker);
            }
        }
    }
}

[System.Serializable]
public class PlayerAttackInfo
{
    public Sprite sprite;
    public string name;
    public PlayerAttackType type;

    [TextArea]
    public string explain;
}

