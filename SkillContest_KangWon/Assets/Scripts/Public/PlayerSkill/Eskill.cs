using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eskill : PlayerSkill
{
    [SerializeField] private float plusHp;
    public override void UseSkill()
    {
        player.PlayerPlusHp(20);
    }
}
